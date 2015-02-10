// --------------------------------------------------------------------------
// <copyright file="RedmineTrackerMonthModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
