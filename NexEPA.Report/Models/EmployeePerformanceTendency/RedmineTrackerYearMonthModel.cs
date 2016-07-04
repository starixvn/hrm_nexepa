// --------------------------------------------------------------------------
// <copyright file="RedmineTrackerYearMonthModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineTrackerYearMonthModel class.
    /// </summary>
    public class RedmineTrackerYearMonthModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int NumberIssue { get; set; }

        public decimal SpentTime { get; set; }
    }
}
