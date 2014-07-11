// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByRoleCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.18">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Employee By Role Criteria.
    /// </summary>
    public class GetEmployeeByRoleCriteria : CriteriaBase
    {
        public int? RoleId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetByRole";
        }
    }
}
