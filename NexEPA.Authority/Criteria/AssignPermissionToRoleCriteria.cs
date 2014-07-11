// -----------------------------------------------------------------------
// <copyright file="AssignPermissionToRoleCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.10">Created</change>
// </history>

namespace NexEPA.Authority.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Assign Permission to Role criteria.
    /// </summary>
    public class AssignPermissionToRoleCriteria : CriteriaBase
    {
        public int RoleId { get; set; }

        public string Functions { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "RoleFunction_Assign";
        }

        public override bool IsValid()
        {
            return RoleId > 0;
        }
    }
}
