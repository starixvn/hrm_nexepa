// --------------------------------------------------------------------------
// <copyright file="RedmineProjectMonthSubTotalModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.13">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineProjectMonthSubTotalModel class.
    /// </summary>
    public class RedmineProjectMonthSubTotalModel
    {
        public int ProjectId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int NumberIssue { get; set; }

        public decimal SpentTime { get; set; }
    }
}
