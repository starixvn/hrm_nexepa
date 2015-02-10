// --------------------------------------------------------------------------
// <copyright file="RedminePersonalEffortDetailViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NexEPA.Report.Models.EmployeePerformanceTendency;

    /// <summary>
    /// RedminePersonalEffortDetailViewModel class.
    /// </summary>
    public class RedminePersonalEffortDetailViewModel
    {
        public decimal GrandTotalWorkload { get; set; }

        public IEnumerable<RedmineProjectTrackerModel> ProjectTrackers { get; set; }

        public IEnumerable<RedminePersonalEffortDetailModel> PersonalEffortDetails { get; set; }

        public IEnumerable<RedmineTrackerMonthModel> DistinctTrackerMonths { get; set; }

        public IEnumerable<RedmineTrackerMonthModel> TrackerMonths { get; set; }

        public IEnumerable<RedmineWorkloadByProjectMonthModel> WorkloadByProjectMonths { get; set; }

        public IEnumerable<RedmineWorkloadByProjectModel> WorkloadByProjects { get; set; }

        public IEnumerable<RedmineWorkloadByMonthModel> WorkloadByMonths { get; set; }
    }
}
