// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByGroupCriteria.cs" company="">
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
    /// Get Employee By Group Criteria.
    /// </summary>
    public class GetEmployeeByGroupCriteria : CriteriaBase
    {
        public int GroupId { get; set; }

        public override bool IsValid()
        {
            return GroupId != 0;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetByGroup";
        }
    }
}
