﻿using DAL.Models;
using QLBH.Common.DAL;
using QLBH.Common.Rsp;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL
{
    public class UserRepository : GenericRep<msistoreContext, User>
    {

        public async Task<SingleRsp> AddUserAsync(User newUser,Userinfo userinfo)
        {
            var res = new SingleRsp();
            try
            {
                using (var context = new msistoreContext())
                {
                    using var transaction = await context.Database.BeginTransactionAsync();
                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    userinfo.UserId = newUser.Id;
                    
                    context.Userinfos.Add(userinfo);
                    await context.SaveChangesAsync();

                    transaction.Commit();
                    
                }
            }
            catch (Exception ex)
            {
                res.SetError($"Error: {ex.Message}");
            }
            return res;
        }
       


        public async Task<List<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            using (var context = new msistoreContext())
            {
                return await context.Users.Where(predicate).ToListAsync();
            }
        }
    }
}