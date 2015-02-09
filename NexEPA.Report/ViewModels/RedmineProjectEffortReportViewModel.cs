// --------------------------------------------------------------------------
// <copyright file="RedmineProjectEffortReportViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.Report.Models.EmployeePerformanceTendency;

    /// <summary>
    /// RedmineProjectEffortReportViewModel class.
    /// </summary>
    public class RedmineProjectEffortReportViewModel
    {
        public decimal GrandTotalSpentTime { get; set; }

        public IEnumerable<RedminePersonalEffortModel> PersonalEfforts { get; set; }

        public IEnumerable<RedmineProjectMonthModel> ProjectMonths { get; set; }

        public IEnumerable<RedmineProjectModel> Projects { get; set; }
    }
}
