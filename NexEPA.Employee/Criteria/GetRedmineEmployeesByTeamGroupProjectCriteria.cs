// -----------------------------------------------------------------------
// <copyright file="GetRedmineEmployeesByTeamGroupProjectCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.21">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get redmine employees by team and group project.
    /// </summary>
    public class GetRedmineEmployeesByTeamGroupProjectCriteria : CriteriaBase
    {
        public int? GroupId { get; set; }

        public int? GroupProjectId { get; set; }

        public string ExcludedGroupId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "EmployeeRedmine_GetByTeamGroupProject";
        }
    }
}