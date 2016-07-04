// --------------------------------------------------------------------------
// <copyright file="RedmineWorkloadByProjectMonthModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineWorkloadByProjectMonthModel class.
    /// </summary>
    public class RedmineWorkloadByProjectMonthModel
    {
        public int ProjectId { get; set; }       

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal Workload { get; set; }
    }
}
