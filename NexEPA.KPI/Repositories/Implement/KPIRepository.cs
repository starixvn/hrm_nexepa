// -----------------------------------------------------------------------
// <copyright file="KPIRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.31">Created</change>
// </history>

namespace NexEPA.KPI.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;
    using NexEPA.KPI.Model;
    using NexEPA.KPI.Repositories.Interface;

    /// <summary>
    /// The implement of KPI repository.
    /// </summary>
    public class KPIRepository : IKPIRepository
    {
        /// <summary>
        /// Get all KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator KPI Model.</returns>
        public IEnumerable<KPIModel> GetKPI(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIModel, KPIGroupModel, TargetType, KPIModel>(
                    (kpi, gr, ta) => 
                    {
                        kpi.Group = gr;
                        kpi.TargetType = ta;
                        return kpi;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get KPI by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator KPI Model.</returns>
        public IEnumerable<KPIModel> GetKPIById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIModel, KPIGroupModel, TargetType, KPIModel>(
                    (kpi, gr, ta) =>
                    {
                        kpi.Group = gr;
                        kpi.TargetType = ta;
                        return kpi;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams());
            }
        }
    }
}
