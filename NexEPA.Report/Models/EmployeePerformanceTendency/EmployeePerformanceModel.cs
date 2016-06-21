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
