// -----------------------------------------------------------------------
// <copyright file="GetKPIGroupByTargetIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.17">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.TargetKPI
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPI Group by TargetId.
    /// </summary>
    public class GetKPIGroupByTargetIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "Target_GetKPIGroup";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
