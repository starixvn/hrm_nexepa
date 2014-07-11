// -----------------------------------------------------------------------
// <copyright file="UpdateTargeEmployeeSettingCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.14">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetEmployeeSetting
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update Target Template Setting criteria.
    /// </summary>
    public class UpdateTargeEmployeeSettingCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public int EPATypeId { get; set; }

        public int? TargetTemplateId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployeeSetting_Update";
        }

        public override bool IsValid()
        {
            return EmployeeId > 0 && EPATypeId > 0;
        }
    }
}
