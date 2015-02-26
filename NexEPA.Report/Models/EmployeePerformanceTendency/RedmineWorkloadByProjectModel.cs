// --------------------------------------------------------------------------
// <copyright file="RedmineWorkloadByProjectModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
