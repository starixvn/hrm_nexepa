// --------------------------------------------------------------------------
// <copyright file="GetEmployeeFlowCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Criteria.FlowManagement
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Employee Flow Criteria.
    /// </summary>
    public class GetEmployeeFlowCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "SettingFlowEmployee_Get";
        }
    }
}
