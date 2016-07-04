// --------------------------------------------------------------------------
// <copyright file="RedmineTrackerModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineTrackerModel class.
    /// </summary>
    public class RedmineTrackerModel
    {
        public int TrackerId { get; set; }

        public string TrackerName { get; set; }

        public int NumberIssue { get; set; }

        public decimal SpentTime { get; set; }
    }
}
