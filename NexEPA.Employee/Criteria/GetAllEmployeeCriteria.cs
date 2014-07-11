// -----------------------------------------------------------------------
// <copyright file="GetAllEmployeeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all employee criteria.
    /// </summary>
    public class GetAllEmployeeCriteria : CriteriaBase
    {
        public bool? IsDisabled { get; set; }

        public string Keyword { get; set; }

        public int? GroupId { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_Get";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
