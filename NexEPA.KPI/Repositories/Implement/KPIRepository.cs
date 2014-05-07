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
    using NexEPA.KPI.Repositories.Interface.KPI;

    /// <summary>
    /// The implement of KPI repository.
    /// </summary>
    public class KPIRepository : IKPIRepository
    {
        /// <summary>
        /// Get all KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List KPI Info.</returns>
        public IEnumerable<KPIInfo> GetKPI(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIInfo, KPIGroupInfo, TargetType, KPIInfo>(
                    (inf, gr, ta) => 
                    {
                        inf.Group = gr;
                        inf.TargetType = ta;
                        return inf;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get KPI by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The IEnumerable KPI Info.</returns>
        public IEnumerable<KPIInfo> GetKPIById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIInfo, KPIGroupInfo, TargetType, KPIInfo>(
                    (inf, gr, ta) =>
                    {
                        inf.Group = gr;
                        inf.TargetType = ta;
                        return inf;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams());
            }
        }
    }
}
