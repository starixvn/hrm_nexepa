// -----------------------------------------------------------------------
// <copyright file="GetEmployeeByKPICriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.07.04">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get employee by KPI criteria.
    /// </summary>
    public class GetEmployeeByKPICriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int KPIId { get; set; }

        public int KPIGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_GetByKPI";
        }

        public override bool IsValid()
        {
            return TargetId > 0 && KPIId > 0 && KPIGroupId > 0;
        }
    }
}
