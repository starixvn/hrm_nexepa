// --------------------------------------------------------------------------
// <copyright file="RedmineTeamAverageWorkloadModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.6">Created</change>
// </history>

namespace NexEPA.Report.Models.TeamEffortReport
{
    using NexEPA.Core.Models;

    /// <summary>
    /// RedmineTeamAverageWorkloadModel class.
    /// </summary>
    public class RedmineTeamAverageWorkloadModel
    {
        public RedmineYearMonthModel YearMonth { get; set; }

        public decimal Workload { get; set; }
    }
}
