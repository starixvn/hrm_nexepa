// --------------------------------------------------------------------------
// <copyright file="MemberEPARepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Repositories.Impl
{
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Repositories.Interfaces;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MemberEPARepository : IMemberEPARepository
    {
        /// <summary>
        /// Get EPA Info.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>EPAInfo Model.</returns>
        public EPAInfoModel GetEPAInfo(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    EPAInfoModel info = multi.Read<EPAInfoModel>().FirstOrDefault();
                    info.ProjectRoles = multi.Read<EPAProjectRoleModel>().ToList();
                    return info;
                }
            }
        }
    }
}
