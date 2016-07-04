// --------------------------------------------------------------------------
// <copyright file="RedmineTrackerMonthModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineTrackerMonthModel class.
    /// </summary>
    public class RedmineTrackerMonthModel
    {
        public int TrackerId { get; set; }

        public string TrackerName { get; set; }

        public int TotalNumberIssue { get; set; }

        public decimal TotalSpentTime { get; set; }

        public RedmineTrackerYearMonthModel YearMonth { get; set; }
    }
}
