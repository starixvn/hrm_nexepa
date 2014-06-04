// --------------------------------------------------------------------------
// <copyright file="UpdateDepartmentCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update Department Criteria.
    /// </summary>
    public class UpdateDepartmentCriteria : CriteriaBase
    {
        public int DepartmentId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int? ManagerId { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return DepartmentId > 0;
        }

        public override string GetSettingKey()
        {
            return "Department_Update";
        }
    }
}
