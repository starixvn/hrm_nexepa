// -----------------------------------------------------------------------
// <copyright file="EmployeePerformanceModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.14">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    using System.Collections.Generic;
    using NexEPA.Employee.Models;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// The model of Employee Performance Tendency report.
    /// </summary>
    public class EmployeePerformanceModel
    {
        public EmployeeModel EmployeeInformation { get; set; }

        public IEnumerable<HistoryProjectViewModel> HistoryProjects { get; set; }

        public IEnumerable<HistoryProjectModel> RawHistoryProjects { get; set; }
    }
}
