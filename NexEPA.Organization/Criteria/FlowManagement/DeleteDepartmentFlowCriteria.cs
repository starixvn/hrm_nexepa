// --------------------------------------------------------------------------
// <copyright file="DeleteDepartmentFlowCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Criteria.FlowManagement
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Delete Department Flow Criteria.
    /// </summary>
    public class DeleteDepartmentFlowCriteria : CriteriaBase
    {
        public int DepartmentId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return DepartmentId > 0;
        }

        public override string GetSettingKey()
        {
            return "SettingFlowDepartment_Delete";
        }
    }
}
