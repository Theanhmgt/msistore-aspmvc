﻿using AutoMapper;
using BLL.DTOs;
using BLL.Token;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.Req;
using Common.Rsp;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QLBH.Common.BLL;
using QLBH.Common.Rsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
    public class UserService : GenericSvc<UserRepository, User>
    {
        private UserRepository userRepository = new UserRepository();
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, IMapper mapper)
        {
            // CLOUDINARY_URL=cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>
            var cloudinaryUrl = "cloudinary://922611133231776:Q0bJhJc_3Z06xk1mFMf0oDSgWxo@dwvg5xlum";
            _cloudinary = new Cloudinary(cloudinaryUrl);
            _cloudinary.Api.Secure = true;
            _mapper = mapper;

            this._configuration = configuration;
        }


        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var users = await userRepository.FindAsync(u => u.Username == username);
            return users.FirstOrDefault();
        }

       

        public async Task<UserDTO> CreateUserAsync(UserReq userReq)
        {

            User newUser = new User
            {
                FirstName = userReq.FirstName,
                LastName = userReq.LastName,
                Email = userReq.Email,
                Username = userReq.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userReq.Password),
                RoleId = userReq.RoleId
            };
            // Upload avatar to Cloudinary
            if (userReq.Avatar is not null)
            {
         
                string avatarUrl = await UploadAvatarAsync(userReq.Avatar);
                newUser.Avatar = avatarUrl;
               
            }
            Userinfo userinfo = new Userinfo
            {
                UserId = newUser.Id
            };
            var result =  await userRepository.AddUserAsync(newUser,userinfo);
            if (!result.Success)
            {
                // Handle error
                throw new Exception("Failed to add userinfo");
            }


            return _mapper.Map<UserDTO>(newUser);
        }

        private async Task<string> UploadAvatarAsync(IFormFile file)
        {
            var tempFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using (var fileStream = File.Create(tempFileName))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(tempFileName),
                Timestamp = DateTime.Now
            };

            ImageUploadResult uploadResult = null;

            try
            {
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            catch (Exception ex)
            {
                var cc = ex;
            }
            finally
            {
                File.Delete(tempFileName);
            }

            return uploadResult?.SecureUrl?.ToString();
        }
      

        public async Task<SingleRsp> AuthenticateJWTAsync(LoginReq loginReq)
        {
            var res = new SingleRsp();

            var users = await userRepository.FindAsync(u => u.Username == loginReq.Username);
            var user = users.FirstOrDefault();

            // Kiểm tra nếu user không tồn tại hoặc mật khẩu không khớp
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginReq.Password, user.Password))
            {
                res.SetError("Invalid credentials.");
                return res;
            }
            var tokenService = new TokenService(_configuration);
            var (accessToken, refreshToken) = tokenService.GenerateJwtToken(user);
            res.Resutls = new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return res;
        }

    }


}
