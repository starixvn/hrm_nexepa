// --------------------------------------------------------------------------
// <copyright file="AddEmployeeFlowCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Criteria.FlowManagement
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Employee Flow Criteria.
    /// </summary>
    public class AddEmployeeFlowCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public string FlowDetails { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return EmployeeId != 0;
        }

        public override string GetSettingKey()
        {
            return "SettingFlowEmployee_Set";
        }
    }
}
