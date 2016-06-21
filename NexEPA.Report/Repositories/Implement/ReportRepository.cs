namespace NexEPA.Report.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Repositories.Interface;

    /// <summary>
    /// The implement of Report Repository.
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        /// <summary>
        /// Get general information of current employee that used in dashboard.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Dashboard Model.</returns>
        public DashboardModel GetGeneralInformationOfCurrentEmployee(ICriteria criteria)
        {
            var dashboardInformation = new DashboardModel();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var generalInformationList = multi.Read<GeneralInformationOfProjectModel>();
                    dashboardInformation.GeneralInformation = generalInformationList != null && generalInformationList.Count() > 0
                                        ? generalInformationList.FirstOrDefault()
                                        : new GeneralInformationOfProjectModel();

                    IEnumerable<OpenProjectModel> openProjects = multi.Read<OpenProjectModel, ProjectModel, EmployeeModel, OpenProjectModel>(
                        (opPro, pro, eva) =>
                        {
                            opPro.Project = pro;
                            opPro.Evaluator = eva;
                            return opPro;
                        },
                        "ProjectId,EmployeeId");
                    dashboardInformation.OpenProjects = openProjects;

                    IEnumerable<HistoryProjectModel> historyProjects = 
                        multi.Read<HistoryProjectModel, ProjectModel, EmployeeModel, ReportMilestoneModel, HistoryProjectModel>(
                        (proHis, pro, eva, mil) =>
                        {
                            proHis.Project = pro;
                            proHis.Evaluator = eva;
                            proHis.Milestone = mil;
                            return proHis;
                        },
                        "ProjectId,EmployeeId,MilestoneId");
                    dashboardInformation.RawHistoryProjects = historyProjects;

                    IEnumerable<KPIModel> listKPIs = multi.Read<KPIModel>();
                    dashboardInformation.ListKPIs = listKPIs;
                }
            }

            return dashboardInformation;
        } 
    }
}
