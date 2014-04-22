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
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Model.Target;
    using NexEPA.KPI.Model.KPI;
    using NexEPA.KPI.Model.KPIGroup;
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
            EPAInfoModel data = new EPAInfoModel();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    data = multi.Read<EPAInfoModel>().FirstOrDefault();
                    data.ProjectRoles = multi.Read<EPAProjectRoleModel>().ToList();
                    List<KPIInfo> kpis = multi.Read<KPIInfo, KPIGroupInfo, TargetType, KPIInfo>(
                        (inf, gr, ta) =>
                        {
                            inf.Group = gr;
                            inf.TargetType = ta;
                            return inf;
                        },
                        "KPIGroupId,TargetTypeId").ToList();

                    List<GroupingKPIModel> groupData = (from kpi in kpis
                                     group kpi by kpi.Group.Name into kpi
                                     select new GroupingKPIModel()
                                    {
                                        KPIGroupName = kpi.Key,
                                        KPIGroupItems = kpi.ToList()
                                    }).ToList();

                    data.KPIInfo = groupData;
                }
            }

            return data;
        }
    }
}
