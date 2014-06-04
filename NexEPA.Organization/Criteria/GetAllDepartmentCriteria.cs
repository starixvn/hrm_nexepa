// --------------------------------------------------------------------------
// <copyright file="GetAllDepartmentCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get All Department Criteria.
    /// </summary>
    public class GetAllDepartmentCriteria : CriteriaBase
    {
        public int? ParentId { get; set; }

        public bool? IsDisabled { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Department_Get";
        }
    }
}
