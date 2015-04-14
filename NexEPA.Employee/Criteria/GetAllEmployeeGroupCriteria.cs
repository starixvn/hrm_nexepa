// -----------------------------------------------------------------------
// <copyright file="GetAllEmployeeGroupCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all employee group criteria.
    /// </summary>
    public class GetAllEmployeeGroupCriteria : CriteriaBase
    {
        public int? IsDisabled { get; set; }

        public string ExcludedGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "EmployeeGroup_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
