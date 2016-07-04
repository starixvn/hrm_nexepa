// --------------------------------------------------------------------------
// <copyright file="RedminePersonalEffortViewModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.EmployeePerformanceTendency;

    /// <summary>
    /// RedminePersonalEffortViewModel class.
    /// </summary>
    public class RedminePersonalEffortViewModel
    {
        public decimal GrandTotalSpentTime { get; set; }

        public IEnumerable<RedminePersonalEffortModel> PersonalEfforts { get; set; }

        public IEnumerable<RedmineProjectMonthModel> ProjectMonths { get; set; }

        public IEnumerable<RedmineProjectModel> Projects { get; set; }
    }
}
