// -----------------------------------------------------------------------
// <copyright file="GetEmployeeByProjectIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.22">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get employee by project ID criteria.
    /// </summary>
    public class GetEmployeeByProjectIdCriteria : CriteriaBase
    {
        public int? ProjectId { get; set; }

        public string Keyword { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_GetByProject";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
