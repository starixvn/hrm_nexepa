// -----------------------------------------------------------------------
// <copyright file="ReportService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using Fanex.NAP.Core.Service.Impl;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;
    using NexEPA.Target.Models;

    /// <summary>
    /// Report Service.
    /// </summary>
    public class ReportService : BaseService, IReportService
    {
        private IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository = null)
        {
            _reportRepository = reportRepository ?? new ReportRepository();
        }

        /// <summary>
        /// Get general information of current employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List DashboardModel.</returns>
        public DashboardModel GetGeneralInformationOfCurrentEmployee(GetGeneralInformationOfCurrentEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var dashboardInformation = new DashboardModel();

            if (criteria.IsValid())
            {
                DashboardModel rawDashboardInformation = _reportRepository.GetGeneralInformationOfCurrentEmployee(criteria);

                if (rawDashboardInformation != null)
                {
                    dashboardInformation.GeneralInformation = rawDashboardInformation.GeneralInformation;

                    dashboardInformation.OpenProjects = rawDashboardInformation.OpenProjects;

                    IEnumerable<HistoryProjectViewModel> projectHistoryList = GetProjectHistory((List<HistoryProjectModel>)rawDashboardInformation.RawHistoryProjects);
                    dashboardInformation.HistoryProjects = projectHistoryList;

                    dashboardInformation.ListKPIs = rawDashboardInformation.ListKPIs;
                }
            }

            return dashboardInformation;
        }

        /// <summary>
        /// Get employee performance tendency report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The ReportEmployeePerformanceTendencyModel.</returns>
        public EmployeePerformanceModel GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformanceReport = new EmployeePerformanceModel();

            if (criteria.IsValid())
            {
                EmployeePerformanceModel rawsEmployeePerformanceReport = _reportRepository.GetEmployeePerformanceTendancyReport(criteria);

                if (rawsEmployeePerformanceReport != null)
                {
                    employeePerformanceReport.EmployeeInformation = rawsEmployeePerformanceReport.EmployeeInformation;

                    IEnumerable<HistoryProjectViewModel> projectHistoryList = GetProjectHistory((List<HistoryProjectModel>)rawsEmployeePerformanceReport.RawHistoryProjects);
                    employeePerformanceReport.HistoryProjects = projectHistoryList;
                }
            }

            return employeePerformanceReport;
        }

        /// <summary>
        /// Get employee performance by team report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The ReportEmployeeByTeamModel.</returns>
        public TeamPerformanceModel GetEmployeePerformanceByTeamReport(GetEmployeePerformanceByTeamCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformanceByTeamReport = new TeamPerformanceModel();

            if (criteria.IsValid())
            {
                employeePerformanceByTeamReport = _reportRepository.GetEmployeePerformanceByTeamReport(criteria);
            }

            return employeePerformanceByTeamReport;
        }

        /// <summary>
        /// Get project history.
        /// </summary>
        /// <param name="rawHistoryProjects">The history projects.</param>
        /// <returns>List HistoryProjectViewModel.</returns>
        private List<HistoryProjectViewModel> GetProjectHistory(List<HistoryProjectModel> rawHistoryProjects)
        {
            var historyProjects = new List<HistoryProjectViewModel>();

            if (rawHistoryProjects != null && rawHistoryProjects.Any())
            {
                historyProjects = LoopAndGetProjectHistory(rawHistoryProjects, historyProjects);
            }

            return historyProjects;
        }

        /// <summary>
        /// Loop and get project history.
        /// </summary>
        /// <param name="rawHistoryProjects">The list history projects.</param>
        /// <param name="historyProjects">The list history projects view model.</param>
        /// <returns>The list History Project View Model.</returns>
        private List<HistoryProjectViewModel> LoopAndGetProjectHistory(
            List<HistoryProjectModel> rawHistoryProjects,
            List<HistoryProjectViewModel> historyProjects)
        {
            for (int i = 0; i < rawHistoryProjects.Count; i++)
            {
                HistoryProjectModel rawHistoryProjectItem = rawHistoryProjects[i];
                var historyProjectItem = new HistoryProjectViewModel
                {
                    Roles = rawHistoryProjectItem.Roles,
                    Score = rawHistoryProjectItem.Score,
                    Comment = rawHistoryProjectItem.Comment,
                    Evaluator = rawHistoryProjectItem.Evaluator,
                    Project = rawHistoryProjectItem.Project
                };

                List<HistoryProjectModel> sameProjectRows = rawHistoryProjects.Where(pro =>
                                    pro.Project.ProjectId == rawHistoryProjectItem.Project.ProjectId).ToList();

                foreach (HistoryProjectModel project in sameProjectRows)
                {
                    var target = new TargetModel
                    {
                        TargetId = project.TargetId,
                        Name = project.Name
                    };

                    historyProjectItem.TargetList.Add(target);

                    if (project != rawHistoryProjectItem)
                    {
                        rawHistoryProjects.Remove(project);
                    }
                }

                historyProjects.Add(historyProjectItem);
            }

            return historyProjects;
        }
    }
}
