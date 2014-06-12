// -----------------------------------------------------------------------
// <copyright file="GetRoleByEmployeeIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.12">Created</change>
// </history>

namespace NexEPA.Authority.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get role by employee ID criteria.
    /// </summary>
    public class GetRoleByEmployeeIdCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_GetRole";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
