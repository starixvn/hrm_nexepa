// -----------------------------------------------------------------------
// <copyright file="TargetRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;
    using NexEPA.Target.Models.Target;
    using NexEPA.Target.Repositories.Interfaces;

    /// <summary>
    /// The implement of Target Repository.
    /// </summary>
    public class TargetRepository : ITargetRepository
    {
        /// <summary>
        /// Get target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>IEnumerable Target Info.</returns>
        public IEnumerable<TargetInfo> GetTarget(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<TargetInfo, TargetType, Project, Milestone, TargetInfo>(
                    (ti, ta, pro, mi) => 
                    {
                        ti.TargetType = ta;
                        ti.Project = pro;
                        ti.Milestone = mi;
                        return ti;
                    },
                    "TargetTypeId,ProjectId,MilestoneId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get Target by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The TargetInfo.</returns>
        public TargetInfo GetTargetById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<TargetInfo, TargetType, Project, Milestone, TargetInfo>(
                    (ti, ta, pro, mi) =>
                    {
                        ti.TargetType = ta;
                        ti.Project = pro;
                        ti.Milestone = mi;
                        return ti;
                    },
                    "TargetTypeId,ProjectId,MilestoneId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get KPI by Target ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The IEnumerable TargetKPIInfo.</returns>
        public IEnumerable<TargetKPIInfo> GetKPIByTargetId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<TargetKPIInfo, TargetKPIGroup, TargetType, TargetKPIInfo>(
                    (ti, tg, tt) =>
                    {
                        ti.Group = tg;
                        ti.TargetType = tt;
                        return ti;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams());
            }
        }
    }
}
