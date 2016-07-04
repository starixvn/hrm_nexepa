// --------------------------------------------------------------------------
// <copyright file="RedmineWorkloadByMonthModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineWorkloadByMonthModel class.
    /// </summary>
    public class RedmineWorkloadByMonthModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; }

        public decimal TotalWorkingDay { get; set; }

        public decimal Workload { get; set; }
    }
}
