// --------------------------------------------------------------------------
// <copyright file="RedmineProjectTrackerModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    using System.Collections.Generic;

    /// <summary>
    /// RedmineProjectTrackerModel class.
    /// </summary>
    public class RedmineProjectTrackerModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int TotalNumberIssue { get; set; }

        public decimal TotalSpentTime { get; set; }

        public int Index { get; set; }

        public RedmineTrackerModel Tracker { get; set; }

        public IList<RedmineTrackerModel> Trackers { get; set; }
    }
}
