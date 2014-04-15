// -----------------------------------------------------------------------
// <copyright file="KPIGroupRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.20">Created</change>
// </history>

namespace NexEPA.KPI.Repository.Implement.KPIGroup
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Model.Target;
    using NexEPA.KPI.Model.KPIGroup;
    using NexEPA.KPI.Repository.Interface.KPIGroup;

    /// <summary>
    /// The implement of KPI Group implement.
    /// </summary>
    public class KPIGroupRepository : IKPIGroupRepository
    {
        /// <summary>
        /// Get all KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List KPI Group.</returns>
        public IList<KPIGroupInfo> GetAllKPIGroup(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIGroupInfo, TargetType, KPIGroupInfo>(
                    (gr, ta) =>
                    {
                        gr.TargetType = ta;
                        return gr;
                    },
                    "TargetTypeId",
                    criteria.GetSpParams()).ToList();
            }
        }

        /// <summary>
        /// Get KPI Group by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Group.</returns>
        public KPIGroupInfo GetKPIGroupById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return (KPIGroupInfo)db.QueryEntity<KPIGroupInfo, TargetType, KPIGroupInfo>(
                    (gr, ta) =>
                    {
                        gr.TargetType = ta;
                        return gr;
                    },
                    "TargetTypeId",
                    criteria.GetSpParams());
            }
        }
    }
}
