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
        public ReportEmployeePerformanceTendencyModel GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformanceReport = new ReportEmployeePerformanceTendencyModel();

            if (criteria.IsValid())
            {
                ReportEmployeePerformanceTendencyModel rawsEmployeePerformanceReport = _reportRepository.GetEmployeePerformanceTendancyReport(criteria);

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
        public ReportEmployeePerformanceByTeamModel GetEmployeePerformanceByTeamReport(GetEmployeePerformanceByTeamCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformanceByTeamReport = new ReportEmployeePerformanceByTeamModel();

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
                for (int i = 0; i < rawHistoryProjects.Count; i++)
                {
                    HistoryProjectModel rawHistoryProjectItem = rawHistoryProjects[i];
                    var historyProjectItem = new HistoryProjectViewModel();

                    List<HistoryProjectModel> sameProjectRows = rawHistoryProjects.Where(pro => pro.Project.ProjectId == rawHistoryProjectItem.Project.ProjectId).ToList();

                    if (sameProjectRows.Count == 1)
                    {
                        // Only evaluate by project
                        historyProjectItem = GetProjectInCaseOnlyEvaluateByProject(rawHistoryProjectItem);
                    }
                    else
                    {
                        List<HistoryProjectModel> sameProjectButNoMilestoneRows = sameProjectRows.Where(pro => pro.Milestone == null).ToList();

                        if (sameProjectButNoMilestoneRows.Count == 1)
                        {
                            // Evaluate by project and milestone
                            historyProjectItem = GetProjectInCaseEvaluateByProjectAndMilestone(
                                                            rawHistoryProjects, 
                                                            rawHistoryProjectItem, 
                                                            sameProjectRows, 
                                                            sameProjectButNoMilestoneRows[0]);
                        }
                        else if (sameProjectButNoMilestoneRows.Count == 0)
                        {
                            // Only evaluate by milestone
                            historyProjectItem = GetProjectInCaseOnlyEvaluateByMilestone(sameProjectRows, rawHistoryProjectItem, rawHistoryProjects);
                        }
                    }

                    historyProjects.Add(historyProjectItem);
                }
            }

            return historyProjects;
        }

        /// <summary>
        /// Get project in case only evaluate by project.
        /// </summary>
        /// <param name="rawHistoryProjectItem">The current history project item.</param>
        /// <returns>The HistoryProjectViewModel.</returns>
        private HistoryProjectViewModel GetProjectInCaseOnlyEvaluateByProject(HistoryProjectModel rawHistoryProjectItem)
        {
            var historyProjectItem = new HistoryProjectViewModel();

            historyProjectItem.TargetId = rawHistoryProjectItem.TargetId;
            historyProjectItem.Roles = rawHistoryProjectItem.Roles;
            historyProjectItem.Score = rawHistoryProjectItem.Score;
            historyProjectItem.Comment = rawHistoryProjectItem.Comment;
            historyProjectItem.ProjectEvaluation = true;
            historyProjectItem.Project = rawHistoryProjectItem.Project;
            historyProjectItem.Evaluator = rawHistoryProjectItem.Evaluator;

            return historyProjectItem;
        }

        /// <summary>
        /// Get project in case evaluate by project and milestone.
        /// </summary>
        /// <param name="rawHistoryProjects">The list history projects.</param>
        /// <param name="rawHistoryProjectItem">The current history project item.</param>
        /// <param name="sameProjectRows">The rows has same project.</param>
        /// <param name="sameProjectButNoMilestoneRow">The rows has same project but no milestone.</param>
        /// <returns>The HistoryProjectViewModel.</returns>
        private HistoryProjectViewModel GetProjectInCaseEvaluateByProjectAndMilestone(
            List<HistoryProjectModel> rawHistoryProjects,
            HistoryProjectModel rawHistoryProjectItem,
            List<HistoryProjectModel> sameProjectRows,
            HistoryProjectModel sameProjectButNoMilestoneRow)
        {
            var historyProjectItem = new HistoryProjectViewModel();

            historyProjectItem.TargetId = sameProjectButNoMilestoneRow.TargetId;
            historyProjectItem.Roles = sameProjectButNoMilestoneRow.Roles;
            historyProjectItem.Score = sameProjectButNoMilestoneRow.Score;
            historyProjectItem.Comment = sameProjectButNoMilestoneRow.Comment;
            historyProjectItem.Project = sameProjectButNoMilestoneRow.Project;
            historyProjectItem.Evaluator = sameProjectButNoMilestoneRow.Evaluator;

            List<HistoryProjectModel> milestoneRows = sameProjectRows.Where(pro => pro.Milestone != null).ToList();

            foreach (HistoryProjectModel milestoneRow in milestoneRows)
            {
                var milestoneItem = new ReportMilestoneModel
                {
                    TargetId = milestoneRow.TargetId,
                    MilestoneId = milestoneRow.Milestone.MilestoneId,
                    Name = milestoneRow.Milestone.Name,
                    StartDate = milestoneRow.Milestone.StartDate,
                    EndDate = milestoneRow.Milestone.EndDate,
                    Score = milestoneRow.Score,
                    Comment = milestoneRow.Comment
                };

                historyProjectItem.Milestones.Add(milestoneItem);

                // Remove project row to avoid processing this project again if it's not current row
                if (milestoneRow != rawHistoryProjectItem)
                {
                    rawHistoryProjects.Remove(milestoneRow);
                }
            }

            // Remove project row to avoid processing this project again if it's not current row
            if (sameProjectButNoMilestoneRow != rawHistoryProjectItem)
            {
                rawHistoryProjects.Remove(sameProjectButNoMilestoneRow);
            }

            return historyProjectItem;
        }

        /// <summary>
        /// Get project in case only evaluate by milestone.
        /// </summary>
        /// <param name="sameProjectRows">The rows has same project.</param>
        /// <param name="rawHistoryProjectItem">The current history project.</param>
        /// <param name="rawHistoryProjects">The list history projects.</param>
        /// <returns>The HistoryProjectViewModel.</returns>
        private HistoryProjectViewModel GetProjectInCaseOnlyEvaluateByMilestone(
            List<HistoryProjectModel> sameProjectRows,
            HistoryProjectModel rawHistoryProjectItem,
            List<HistoryProjectModel> rawHistoryProjects)
        {
            var historyProjectItem = new HistoryProjectViewModel();

            historyProjectItem.TargetId = sameProjectRows[0].TargetId;
            historyProjectItem.Roles = sameProjectRows[0].Roles;
            historyProjectItem.MilestoneEvaluation = true;
            historyProjectItem.Project = sameProjectRows[0].Project;
            historyProjectItem.Evaluator = sameProjectRows[0].Evaluator;

            foreach (HistoryProjectModel milestone in sameProjectRows)
            {
                milestone.Milestone.TargetId = milestone.TargetId;
                milestone.Milestone.Score = milestone.Score;
                milestone.Milestone.Comment = milestone.Comment;
                historyProjectItem.Milestones.Add(milestone.Milestone);

                // Remove project row to avoid processing this project again if it's not current row
                if (milestone != rawHistoryProjectItem)
                {
                    rawHistoryProjects.Remove(milestone);
                }
            }

            return historyProjectItem;
        }
    }
}
