// --------------------------------------------------------------------------
// <copyright file="AddDepartmentCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Department Criteria.
    /// </summary>
    public class AddDepartmentCriteria : CriteriaBase
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int? ManagerId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public override bool IsValid()
        {
            return string.IsNullOrEmpty(Code) && string.IsNullOrEmpty(Name);
        }

        public override string GetSettingKey()
        {
            return "Department_Insert";
        }
    }
}
