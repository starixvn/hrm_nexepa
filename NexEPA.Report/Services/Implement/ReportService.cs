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
    using NexEPA.Employee.Models;
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
        /// <returns>The List ProjectHistoryViewModel.</returns>
        public List<object> GetGeneralInformationOfCurrentEmployee(
            GetGeneralInformationOfCurrentEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var generalInformation = new List<object>();

            if (criteria.IsValid())
            {
                List<object> rawsGeneralInformation = _reportRepository.GetGeneralInformationOfCurrentEmployee(criteria);

                if (rawsGeneralInformation != null && rawsGeneralInformation.Count > 3)
                {
                    // Get General Information
                    generalInformation.Add(rawsGeneralInformation[0]);

                    // Get Open Project
                    generalInformation.Add(rawsGeneralInformation[1]);

                    // Get History Project
                    List<HistoryProjectViewModel> projectHistoryList = GetProjectHistory((List<HistoryProjectModel>)rawsGeneralInformation[2]);
                    generalInformation.Add(projectHistoryList);

                    // Get KPI Information
                    generalInformation.Add(rawsGeneralInformation[3]);
                }
            }

            return generalInformation;
        }

        /// <summary>
        /// Get employee performance tendency report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list object.</returns>
        public List<object> GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformaceReport = new List<object>();

            if (criteria.IsValid())
            {
                List<object> rawsEmployeePerformaceReport = _reportRepository.GetEmployeePerformanceTendancyReport(criteria);

                if (rawsEmployeePerformaceReport != null && rawsEmployeePerformaceReport.Count >= 1)
                {
                    // Get Employee Information
                    ReportEmployeeModel employee = GetEmployeeGeneralInformation((EmployeeModel)rawsEmployeePerformaceReport[0]);
                    employeePerformaceReport.Add(employee);

                    // Get History Project
                    List<HistoryProjectViewModel> projectHistoryList = GetProjectHistory((List<HistoryProjectModel>)rawsEmployeePerformaceReport[1]);
                    employeePerformaceReport.Add(projectHistoryList);
                }
            }

            return employeePerformaceReport;
        }

        /// <summary>
        /// Get employee performance by team report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list ReportEmployeeByTeamModel.</returns>
        public IEnumerable<ReportEmployeeByTeamModel> GetEmployeePerformanceByTeamReport(GetEmployeePerformanceByTeamCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<ReportEmployeeByTeamModel> employeePerformanceByTeamReport = null;

            if (criteria.IsValid())
            {
                List<object> rawsEmployeeByTeamReport = _reportRepository.GetEmployeePerformanceByTeamReport(criteria);

                if (rawsEmployeeByTeamReport != null && rawsEmployeeByTeamReport.Count >= 1)
                {
                    employeePerformanceByTeamReport = (IEnumerable<ReportEmployeeByTeamModel>)rawsEmployeeByTeamReport[0];
                }
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

            if (rawHistoryProjects != null && rawHistoryProjects.Count > 0)
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
                                                            sameProjectButNoMilestoneRows);
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
        /// <param name="sameProjectButNoMilestoneRows">The rows has same project but no milestone.</param>
        /// <returns>The HistoryProjectViewModel.</returns>
        private HistoryProjectViewModel GetProjectInCaseEvaluateByProjectAndMilestone(
            List<HistoryProjectModel> rawHistoryProjects,
            HistoryProjectModel rawHistoryProjectItem,
            List<HistoryProjectModel> sameProjectRows,
            List<HistoryProjectModel> sameProjectButNoMilestoneRows)
        {
            var historyProjectItem = new HistoryProjectViewModel();

            historyProjectItem.TargetId = sameProjectButNoMilestoneRows[0].TargetId;
            historyProjectItem.Roles = sameProjectButNoMilestoneRows[0].Roles;
            historyProjectItem.Score = sameProjectButNoMilestoneRows[0].Score;
            historyProjectItem.Comment = sameProjectButNoMilestoneRows[0].Comment;
            historyProjectItem.Project = sameProjectButNoMilestoneRows[0].Project;
            historyProjectItem.Evaluator = sameProjectButNoMilestoneRows[0].Evaluator;

            List<HistoryProjectModel> milestoneRows = sameProjectRows.Where(pro => pro.Milestone != null).ToList();
            var milestones = new List<ReportMilestoneModel>();

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

                milestones.Add(milestoneItem);

                // Remove project row to avoid processing this project again if it's not current row
                if (milestoneRow != rawHistoryProjectItem)
                {
                    rawHistoryProjects.Remove(milestoneRow);
                }
            }

            historyProjectItem.Milestones = milestones;

            // Remove project row to avoid processing this project again if it's not current row
            if (sameProjectButNoMilestoneRows[0] != rawHistoryProjectItem)
            {
                rawHistoryProjects.Remove(sameProjectButNoMilestoneRows[0]);
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

        /// <summary>
        /// Get general information of employee.
        /// </summary>
        /// <param name="rawEmployee">The raw employee.</param>
        /// <returns>The ReportEmployeeModel.</returns>
        private ReportEmployeeModel GetEmployeeGeneralInformation(EmployeeModel rawEmployee)
        {
            var employee = new ReportEmployeeModel
            {
                EmployeeId = rawEmployee.EmployeeId,
                Code = rawEmployee.Code,
                DomainUserName = rawEmployee.DomainUsername,
                FullName = rawEmployee.FullName,
                NickName = rawEmployee.NickName,
                JobTitle = rawEmployee.JobTitle.Name,
                Roles = rawEmployee.Roles,
                TeamName = rawEmployee.Group.Name
            };

            return employee;
        }
    }
}
