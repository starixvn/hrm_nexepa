// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByDepartmentCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.20">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Employee By Department Criteria.
    /// </summary>
    public class GetEmployeeByDepartmentCriteria : CriteriaBase
    {
        public int DepartmentId { get; set; }

        public override bool IsValid()
        {
            return DepartmentId != 0;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetByDepartment";
        }
    }
}
