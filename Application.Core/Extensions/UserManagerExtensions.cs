using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Domain.Entities;

namespace Application.Core.Extensions
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Find user by user name include related entity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="userName"></param>
        /// <param name="navigationPropertyPath">Navigation property expression</param>
        /// <returns></returns>
        public static async Task<TUser> FindByNameAsync<TUser, TProperty>(this UserManager<TUser> source, string userName,
            Expression<Func<TUser, TProperty>> navigationPropertyPath) where TUser : ApplicationUser
        {
            return await source.Users.Include(navigationPropertyPath)
                .Where(x => x.NormalizedUserName == source.NormalizeName(userName))
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Find user by refresh token include related entity.
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="token">Refresh token</param>
        /// <param name="navigationPropertyPath">Navigation property expression</param>
        /// <returns></returns>
        public static async Task<TUser> FindByRefreshTokenAsync<TUser, TProperty>(this UserManager<TUser> source, string token,
            Expression<Func<TUser, TProperty>> navigationPropertyPath) where TUser : ApplicationUser
        {
            return await source.Users.Include(navigationPropertyPath)
                .Where(x => x.RefreshTokens.Any(rt => rt.Token == token))
                .SingleOrDefaultAsync();
        }
    }
}
