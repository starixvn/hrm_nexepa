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
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Core.Utilities;
    using NexEPA.KPI.Model;
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

        private IEncodeService _encodeService;

        public ReportService()
            : this(null, null)
        {
        }

        public ReportService(IReportRepository reportRepository, IEncodeService encodeService)
        {
            _reportRepository = reportRepository ?? new ReportRepository();
            _encodeService = encodeService ?? new EncodeService();
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

                    dashboardInformation.ListKPIs = GetKPIsInformation(rawDashboardInformation.ListKPIs);
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
                    string gravatarString = string.Empty;
                    employeePerformanceReport.EmployeeInformation = rawsEmployeePerformanceReport.EmployeeInformation;

                    var userEmail = employeePerformanceReport.EmployeeInformation.Email;
                    if (userEmail != null)
                    {
                        gravatarString = GravatarImage.HashEmailForGravatar(userEmail);
                    }

                    employeePerformanceReport.EmployeeInformation.Gravatar = gravatarString;

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
            var teamReport = new TeamPerformanceModel();

            if (criteria.IsValid())
            {
                teamReport = _reportRepository.GetEmployeePerformanceByTeamReport(criteria);
                teamReport.ReportByProject = GroupMember(teamReport.ReportByProject);
            }

            return teamReport;
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
                    Comment = rawHistoryProjectItem.Comment,
                    Evaluator = rawHistoryProjectItem.Evaluator,
                    Project = rawHistoryProjectItem.Project
                };

                List<HistoryProjectModel> sameProjectRows = rawHistoryProjects.Where(pro =>
                                    pro.Project.ProjectId == rawHistoryProjectItem.Project.ProjectId).ToList();

                decimal averageScore = 0;
                int numberRowsSameProject = sameProjectRows.Count();

                foreach (HistoryProjectModel project in sameProjectRows)
                {
                    var target = new TargetModel
                    {
                        TargetId = project.TargetId,
                        Name = project.Name
                    };

                    historyProjectItem.TargetList.Add(target);
                    string plainScoreText = !string.IsNullOrEmpty(project.Score) ? _encodeService.Decode(project.Score) : "0";
                    decimal projectScore = decimal.Parse(plainScoreText);
                    averageScore += projectScore;

                    if (project != rawHistoryProjectItem)
                    {
                        rawHistoryProjects.Remove(project);
                    }
                }

                averageScore = averageScore / numberRowsSameProject;
                historyProjectItem.Score = averageScore;

                historyProjects.Add(historyProjectItem);
            }

            return historyProjects;
        }

        /// <summary>
        /// Get KPIs information.
        /// </summary>
        /// <param name="listKPIs">The list KPIs.</param>
        /// <returns>The enumerator KPI model.</returns>
        private IEnumerable<KPIModel> GetKPIsInformation(IEnumerable<KPIModel> listKPIs)
        {
            IEnumerable<KPIModel> groupKPIs = from kpi in listKPIs
                                               group kpi by kpi.KPIId into groupKPI
                                               select new KPIModel
                                               {
                                                   KPIId = groupKPI.Key,
                                                   Name = groupKPI.First().Name,
                                                   EvaluateTime = groupKPI.Count(),
                                                   AverageScore = CalculateAverageScoreOfKPI(groupKPI)
                                               };

            return groupKPIs;
        }

        /// <summary>
        /// Calculate average score of KPI.
        /// </summary>
        /// <param name="groupKPI">The group KPI.</param>
        /// <returns>The average score.</returns>
        private decimal CalculateAverageScoreOfKPI(IGrouping<int, KPIModel> groupKPI)
        {
            decimal totalScore = 0;

            foreach (KPIModel kpi in groupKPI)
            {
                string itemScoreText = !string.IsNullOrEmpty(kpi.Score) ? _encodeService.Decode(kpi.Score).Replace("_" + kpi.KPIId, string.Empty) : string.Empty;
                decimal itemScore = !string.IsNullOrEmpty(itemScoreText) ? decimal.Parse(itemScoreText) : 0;
                totalScore += itemScore;
            }

            decimal averageScore = groupKPI.Count() > 0 ? totalScore / groupKPI.Count() : 0;
            return averageScore;
        }

        /// <summary>
        /// Group member.
        /// </summary>
        /// <param name="members">The list member.</param>
        /// <returns>The enumerator members.</returns>
        private IEnumerable<EmployeePerformanceByProjectModel> GroupMember(
            IEnumerable<EmployeePerformanceByProjectModel> members)
        {
            IEnumerable<EmployeePerformanceByProjectModel> groupMembers = from member in members
                                                    group member by member.EmployeeId into groupMember
                                                    select new EmployeePerformanceByProjectModel
                                                    {
                                                        EmployeeId = groupMember.Key,
                                                        NickName = groupMember.First().NickName,
                                                        FullName = groupMember.First().FullName,
                                                        JoinedDate = groupMember.First().JoinedDate,
                                                        Group = groupMember.First().Group,
                                                        ProjectCount = groupMember.Count(),
                                                        EPACount = groupMember.Count(),
                                                        AverageScore = CalculateAverageScoreOfMember(groupMember)
                                                    };
            return groupMembers;
        }

        /// <summary>
        /// Calculate average score of member.
        /// </summary>
        /// <param name="groupMember">The group member.</param>
        /// <returns>The average score.</returns>
        private string CalculateAverageScoreOfMember(IGrouping<int, EmployeePerformanceByProjectModel> groupMember)
        {
            decimal totalScore = 0;

            foreach (EmployeePerformanceByProjectModel member in groupMember)
            {
                string memberScoreText = !string.IsNullOrEmpty(member.AverageScore)
                    ? _encodeService.Decode(member.AverageScore) : string.Empty;
                decimal memberScore = !string.IsNullOrEmpty(memberScoreText) ? decimal.Parse(memberScoreText) : 0;
                totalScore += memberScore;
            }

            decimal averageScore = groupMember.Count() > 0 ? totalScore / groupMember.Count() : 0;
            return averageScore > 0 ? averageScore.ToString() : string.Empty;
        }
    }
}
