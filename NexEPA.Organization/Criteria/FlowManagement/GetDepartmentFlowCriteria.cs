// --------------------------------------------------------------------------
// <copyright file="GetDepartmentFlowCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Criteria.FlowManagement
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Department Flow Criteria.
    /// </summary>
    public class GetDepartmentFlowCriteria : CriteriaBase
    {
        public int DepartmentId { get; set; }

        public override bool IsValid()
        {
            return DepartmentId > 0;
        }

        public override string GetSettingKey()
        {
            return "SettingFlowDepartment_Get";
        }
    }
}
