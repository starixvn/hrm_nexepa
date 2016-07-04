// --------------------------------------------------------------------------
// <copyright file="CommonRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Web.Repositories.Impl
{
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Authority.Models;
    using NexEPA.Web.Models;
    using NexEPA.Web.Repositories.Interfaces;

    /// <summary>
    /// Common Repository.
    /// </summary>
    public class CommonRepository : ICommonRepository
    {
        /// <summary>
        /// Function to get user by domain user.
        /// </summary>
        /// <param name="criteria">The Criteria.</param>
        /// <returns>User Session.</returns>
        public UserSession GetUserByDomainUsername(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    UserSession user = multi.Read<UserSession>().FirstOrDefault();
                    user.Roles = multi.Read<UserRole>().ToList();
                    user.Functions = multi.Read<FunctionModel>().ToList();
                    user.MenuItems = multi.Read<MenuItem>().ToList();

                    return user;
                }
            }
        }
    }
}
