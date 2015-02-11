// --------------------------------------------------------------------------
// <copyright file="AuthorityRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.11">Created</change>
// </history>

namespace NexEPA.Authority.Repositories.Implement
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Authority.Repositories;

    /// <summary>
    /// AuthorityRepository class.
    /// </summary>
    public class AuthorityRepository : IAuthorityRepository
    {
        /// <summary>
        /// Determines whether [is only member] [the specified criteria].
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return true if employee has only member role otherwise return false.</returns>
        public bool IsOnlyMember(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                var outParams = db.GetOuputParams();
                bool isOnlyMember = (bool)outParams["isonlymember"];

                return isOnlyMember;
            }
        }
    }
}
