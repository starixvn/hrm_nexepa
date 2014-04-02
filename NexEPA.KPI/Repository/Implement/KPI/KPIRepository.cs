// -----------------------------------------------------------------------
// <copyright file="KPIRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.31">Created</change>
// </history>

namespace NexEPA.KPI.Repository.Implement.KPI
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Model.Target;
    using NexEPA.KPI.Model.KPI;
    using NexEPA.KPI.Model.KPIGroup;
    using NexEPA.KPI.Repository.Interface.KPI;

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
        public IList<KPIInfo> GetKPI(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return (IList<KPIInfo>)db.Query<KPIInfo, KPIGroupInfo, TargetType, KPIInfo>(
                    (inf, gr, ta) => 
                    {
                        inf.Group = gr;
                        inf.TargetType = ta;
                        return inf;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams()).ToList();
            }
        }

        /// <summary>
        /// Get KPI by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Info.</returns>
        public KPIInfo GetKPIById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<KPIInfo, KPIGroupInfo, TargetType, KPIInfo>(
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
