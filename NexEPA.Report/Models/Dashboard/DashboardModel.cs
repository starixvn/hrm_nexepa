namespace NexEPA.Report.Models.Dashboard
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// The model of dashboard will return to view.
    /// </summary>
    public class DashboardModel
    {
        public GeneralInformationOfProjectModel GeneralInformation { get; set; }

        public IEnumerable<OpenProjectModel> OpenProjects { get; set; }

        public IEnumerable<HistoryProjectViewModel> HistoryProjects { get; set; }

        public IEnumerable<HistoryProjectModel> RawHistoryProjects { get; set; }

        public IEnumerable<KPIModel> ListKPIs { get; set; }
    }
}
