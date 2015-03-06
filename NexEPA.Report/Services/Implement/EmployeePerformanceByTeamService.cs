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
                reportData.MemberProjects = GetDistinctMemberProjects(reportData.MemberProjects);
                reportData.SubTotalSpentTimeByMembers = GetSubTotalSpentTimeByMembers(reportData.MemberSubTotals);
                reportData.SubTotalWorkingDayByMembers = GetSubTotalWorkingDayByMembers(reportData.MemberWorkingDays);
                reportData.SubTotalWorkloadByMembers = CalculateSubTotalWorkloadByMembers(reportData.SubTotalSpentTimeByMembers, reportData.SubTotalWorkingDayByMembers);
                reportData.AverageWorkloads = CalculateAverageWorkloads(reportData.TotalEfforts, reportData.MemberWorkingDays);
                reportData.GrandTotalEffort = reportData.TeamEfforts.Sum(te => te.SpentTime);
                reportData.GrandAverageWorkload = CalculateGrandAverageWorkload(reportData.GrandTotalEffort, reportData.MemberWorkingDays);
            }

            return reportData;
        }

        /// <summary>
        /// Calculates the grand average workload.
        /// </summary>
        /// <param name="grandTotalEffort">The grand total effort.</param>
        /// <param name="memberWorkingDays">The member working days.</param>
        /// <returns>The grand average workload.</returns>
        private decimal CalculateGrandAverageWorkload(decimal grandTotalEffort, IEnumerable<RedmineMemberWorkingDayModel> memberWorkingDays)
        {
            decimal grandWorkingDay = memberWorkingDays.Sum(w => w.WorkingDay);
            decimal result = grandWorkingDay == 0 ? 0 : grandTotalEffort / (8 * grandWorkingDay);

            return result;
        }

        /// <summary>
        /// Calculates the average workloads.
        /// </summary>
        /// <param name="totalEfforts">The total efforts.</param>
        /// <param name="memberWorkingDays">The member working days.</param>
        /// <returns>The average workloads.</returns>
        private IEnumerable<RedmineTeamAverageWorkloadModel> CalculateAverageWorkloads(
            IEnumerable<RedmineTeamTotalEffortModel> totalEfforts,
            IEnumerable<RedmineMemberWorkingDayModel> memberWorkingDays)
        {
            var result = new List<RedmineTeamAverageWorkloadModel>();

            foreach (RedmineTeamTotalEffortModel totalEffort in totalEfforts)
            {
                decimal workingDay = memberWorkingDays
                                    .Where(w => w.YearMonth.Year == totalEffort.YearMonth.Year && w.YearMonth.Month == totalEffort.YearMonth.Month)
                                    .Sum(w => w != null ? w.WorkingDay : 0);

                var averageWorkload = new RedmineTeamAverageWorkloadModel
                {
                    YearMonth = totalEffort.YearMonth,
                    Workload = workingDay == 0 ? 0 : totalEffort.Total / (8 * workingDay)
                };

                result.Add(averageWorkload);
            }

            return result;
        }

        /// <summary>
        /// Calculate sub total workload by members.
        /// </summary>
        /// <param name="subTotalSpentTimeByMembers">Collection sub total spent time by members.</param>
        /// <param name="subTotalWorkingDayByMembers">Collection sub total working day by members.</param>
        /// <returns>Collection sub total workload by members.</returns>
        private IEnumerable<RedmineEmployeeModel> CalculateSubTotalWorkloadByMembers(
            IEnumerable<RedmineEmployeeModel> subTotalSpentTimeByMembers,
            IEnumerable<RedmineEmployeeModel> subTotalWorkingDayByMembers)
        {
            var result = new List<RedmineEmployeeModel>();

            foreach (RedmineEmployeeModel workingDay in subTotalWorkingDayByMembers)
            {
                decimal? spentTime = subTotalSpentTimeByMembers.Where(st => st.NickName.ToUpperInvariant() == workingDay.NickName.ToUpperInvariant())
                                    .Select(st => st.SubTotalSpentTime).FirstOrDefault();

                var employee = new RedmineEmployeeModel
                {
                    NickName = workingDay.NickName,
                    FullName = workingDay.FullName,
                    SubTotalWorkload = workingDay.SubTotalWorkingDay == 0 ? 0 : ((spentTime ?? 0) / (8 * workingDay.SubTotalWorkingDay))
                };

                result.Add(employee);
            }

            return result.ToList();
        }

        /// <summary>
        /// Gets the sub total working day by members.
        /// </summary>
        /// <param name="memberWorkingDays">The member working days.</param>
        /// <returns>The sub total working day.</returns>
        private IEnumerable<RedmineEmployeeModel> GetSubTotalWorkingDayByMembers(IEnumerable<RedmineMemberWorkingDayModel> memberWorkingDays)
        {
            var result = memberWorkingDays.GroupBy(s => new { s.Employee.NickName, s.Employee.FullName })
                 .Select(s => new RedmineEmployeeModel
                 {
                     FullName = s.Key.FullName,
                     NickName = s.Key.NickName,
                     SubTotalWorkingDay = memberWorkingDays.Where(m => m.Employee.NickName == s.Key.NickName).Sum(m => m.WorkingDay)
                 });

            return result.ToList();
        }

        /// <summary>
        /// Gets the sub total spent time by members.
        /// </summary>
        /// <param name="memberSubTotals">The member sub totals.</param>
        /// <returns>The sub total spent time.</returns>
        private List<RedmineEmployeeModel> GetSubTotalSpentTimeByMembers(IEnumerable<RedmineMemberSubTotalModel> memberSubTotals)
        {
            var result = memberSubTotals.GroupBy(s => new { s.Employee.NickName, s.Employee.FullName })
                .Select(s => new RedmineEmployeeModel
                {
                    FullName = s.Key.FullName,
                    NickName = s.Key.NickName,
                    SubTotalSpentTime = memberSubTotals.Where(m => m.Employee.NickName == s.Key.NickName).Sum(m => m.SpentTime)
                });

            return result.ToList();
        }

        /// <summary>
        /// Gets the distinct member projects.
        /// </summary>
        /// <param name="memberProjects">The member projects.</param>       
        /// <returns>RedmineMemberProjectModel collection.</returns>
        private List<RedmineMemberProjectModel> GetDistinctMemberProjects(IEnumerable<RedmineMemberProjectModel> memberProjects)
        {
            var index = 1;
            var result = memberProjects.GroupBy(m => new { m.Employee.NickName, m.Employee.FullName })
                .Select(m => new RedmineMemberProjectModel
                {
                    Index = index++,
                    Employee = new RedmineEmployeeModel
                    {
                        NickName = m.Key.NickName,
                        FullName = m.Key.FullName
                    },
                    Projects = memberProjects.Where(p => p.Employee.NickName == m.Key.NickName && p.Project != null)
                                .Select(p => new RedmineProjectModel
                                {
                                    ProjectId = p.Project.ProjectId,
                                    ProjectName = p.Project.ProjectName,
                                    TotalSpentTimeOfAllMonths = p.SubTotal
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
                                                                              ProjectCount = groupMember.GroupBy(g => new { g.ProjectCount }).Count(),
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
