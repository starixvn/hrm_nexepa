namespace NexEPA.Report.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// The interface of Report Service.
    /// </summary>
    public interface IReportService
    {
        DashboardModel GetGeneralInformationOfCurrentEmployee(GetGeneralInformationOfCurrentEmployeeCriteria criteria);

        List<HistoryProjectViewModel> GetProjectHistory(List<HistoryProjectModel> rawHistoryProjects);
    }
}
