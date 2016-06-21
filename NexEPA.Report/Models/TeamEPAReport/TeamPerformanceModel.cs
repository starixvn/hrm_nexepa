namespace NexEPA.Report.Models.TeamEPAReport
{
    using System.Collections.Generic;

    /// <summary>
    /// The model of employee performance by team report.
    /// </summary>
    public class TeamPerformanceModel
    {
        public IEnumerable<EmployeePerformanceByProjectModel> ReportByProject { get; set; }
    }
}
