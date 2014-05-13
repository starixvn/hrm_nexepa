// -----------------------------------------------------------------------
// <copyright file="KPIGroupRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.20">Created</change>
// </history>

namespace NexEPA.KPI.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;
    using NexEPA.KPI.Model;
    using NexEPA.KPI.Repositories.Interface.KPIGroup;

    /// <summary>
    /// The implement of KPI Group implement.
    /// </summary>
    public class KPIGroupRepository : IKPIGroupRepository
    {
        /// <summary>
        /// Get all KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>IEnumerable KPI Group.</returns>
        public IEnumerable<KPIGroupModel> GetAllKPIGroup(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIGroupModel, TargetType, KPIGroupModel>(
                    (gr, ta) =>
                    {
                        gr.TargetType = ta;
                        return gr;
                    },
                    "TargetTypeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get KPI Group by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Group.</returns>
        public KPIGroupModel GetKPIGroupById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<KPIGroupModel, TargetType, KPIGroupModel>(
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
