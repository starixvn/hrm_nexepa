// --------------------------------------------------------------------------
// <copyright file="RedmineTeamEffortModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceByTeam
{
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// RedmineTeamEffortModel class.
    /// </summary>
    public class RedmineTeamEffortModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineProjectModel Project { get; set; }

        public RedmineYearMonthModel YearMonth { get; set; }

        public decimal SpentTime { get; set; }
    }
}
