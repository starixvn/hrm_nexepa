// --------------------------------------------------------------------------
// <copyright file="DeleteDepartmentCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.10">Created</change>
// </history>

namespace NexEPA.Organization.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Delete Department Criteria.
    /// </summary>
    public class DeleteDepartmentCriteria : CriteriaBase
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
            return "Department_Delete";
        }
    }
}
