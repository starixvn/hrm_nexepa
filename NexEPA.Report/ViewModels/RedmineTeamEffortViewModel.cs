// --------------------------------------------------------------------------
// <copyright file="RedmineTeamEffortViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;

    /// <summary>
    /// RedmineTeamEffortViewModel class.
    /// </summary>
    public class RedmineTeamEffortViewModel
    {
        public IEnumerable<RedmineTeamEffortModel> TeamEfforts { get; set; }

        public IEnumerable<RedmineMemberProjectModel> MemberProjects { get; set; }

        public IEnumerable<RedmineYearMonthModel> YearMonths { get; set; }

        public IEnumerable<RedmineMemberSubTotalModel> MemberSubTotals { get; set; }

        public IEnumerable<RedmineMemberWorkingDayModel> MemberWorkingDays { get; set; }

        public IEnumerable<RedmineMemberWorkloadModel> MemberWorkloads { get; set; }

        public IEnumerable<RedmineTeamTotalEffortModel> TotalEfforts { get; set; }

        public decimal GrandTotalEffort { get; set; }
    }
}
