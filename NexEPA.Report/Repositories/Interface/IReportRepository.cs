namespace NexEPA.Report.Repositories.Interface
{
    using Fanex.Data.Repository;
    using NexEPA.Report.Models.Dashboard;

    /// <summary>
    /// The interface of Report Repository.
    /// </summary>
    public interface IReportRepository
    {
        DashboardModel GetGeneralInformationOfCurrentEmployee(ICriteria criteria);
    }
}
