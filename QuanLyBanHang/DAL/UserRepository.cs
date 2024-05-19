﻿using DAL.Models;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class UserRepository : GenericRep<msistoreContext, User>
    {
        public async Task<SingleRsp> AddUserAsync(User newUser, Userinfo userinfo)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    await context.Users.AddAsync(newUser);
                    await context.Userinfos.AddAsync(userinfo);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                res.SetError($"Error: {ex.Message}");
            }
            return res;
        }
    }
}