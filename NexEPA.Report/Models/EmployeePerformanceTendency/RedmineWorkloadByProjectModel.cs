// --------------------------------------------------------------------------
// <copyright file="RedmineWorkloadByProjectModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineWorkloadByProjectModel class.
    /// </summary>
    public class RedmineWorkloadByProjectModel
    {
        public int ProjectId { get; set; }

        public decimal Workload { get; set; }
    }
}
