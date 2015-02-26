// --------------------------------------------------------------------------
// <copyright file="EmployeePerformanceByTeamService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// EmployeePerformanceByTeamService class.
    /// </summary>
    public class EmployeePerformanceByTeamService : BaseService, IEmployeePerformanceByTeamService
    {
        private IEmployeePerformanceByTeamRepository _repository;
        private IEncodeService _encodeService;

        public EmployeePerformanceByTeamService()
            : this(null, null)
        {
        }

        public EmployeePerformanceByTeamService(IEmployeePerformanceByTeamRepository repository, IEncodeService encodeService)
        {
            _repository = repository ?? new EmployeePerformanceByTeamRepository();
            _encodeService = encodeService ?? new EncodeService();
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
                teamReport = _repository.GetEmployeePerformanceByTeamReport(criteria);
                teamReport.ReportByProject = GroupMember(teamReport.ReportByProject);
            }

            return teamReport;
        }

        /// <summary>
        /// Gets the redmine team effort.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedmineTeamEffortViewModel data.</returns>
        public RedmineTeamEffortViewModel GetRedmineTeamEffort(GetRedmineTeamEffortCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var reportData = new RedmineTeamEffortViewModel();

            if (criteria.IsValid())
            {
                reportData = _repository.GetRedmineTeamEffort(criteria);
                reportData.MemberProjects = GetDistinctMemberProjects(reportData.MemberProjects, reportData.TeamEfforts);
                reportData.GrandTotalEffort = reportData.TeamEfforts.Sum(te => te.SpentTime);
            }

            return reportData;
        }

        /// <summary>
        /// Gets the distinct member projects.
        /// </summary>
        /// <param name="memberProjects">The member projects.</param>
        /// <param name="teamEfforts">The team efforts.</param>
        /// <returns>RedmineMemberProjectModel collection.</returns>
        private List<RedmineMemberProjectModel> GetDistinctMemberProjects(
            IEnumerable<RedmineMemberProjectModel> memberProjects,
            IEnumerable<RedmineTeamEffortModel> teamEfforts)
        {
            var result = memberProjects.GroupBy(m => new { m.Employee.NickName, m.Employee.FullName })
                .Select(m => new RedmineMemberProjectModel
                {
                    Employee = new RedmineEmployeeModel
                    {
                        NickName = m.Key.NickName,
                        FullName = m.Key.FullName
                    },
                    Projects = memberProjects.Where(p => p.Employee.NickName == m.Key.NickName)
                                .Select(p => new RedmineProjectModel
                                {
                                    ProjectId = p.Project.ProjectId,
                                    ProjectName = p.Project.ProjectName,
                                    TotalSpentTimeOfAllMonths = teamEfforts.Where(
                                                        te => te.Employee.NickName == m.Key.NickName
                                                        && te.Project.ProjectId == p.Project.ProjectId).Sum(te => te.SpentTime)
                                }).ToList()
                });

            return result.ToList();
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
