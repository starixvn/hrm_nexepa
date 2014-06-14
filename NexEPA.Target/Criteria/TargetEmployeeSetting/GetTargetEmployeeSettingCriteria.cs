// -----------------------------------------------------------------------
// <copyright file="GetTargetEmployeeSettingCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.14">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetEmployeeSetting
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get target employee setting criteria.
    /// </summary>
    public class GetTargetEmployeeSettingCriteria : CriteriaBase
    {
        public int EPATypeId { get; set; }

        public int? DepartmentId { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployeeSetting_Get";
        }

        public override bool IsValid()
        {
            return EPATypeId > 0;
        }
    }
}
