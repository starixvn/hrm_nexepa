// --------------------------------------------------------------------------
// <copyright file="EmployeePerformanceByTeamRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// EmployeePerformanceByTeamRepository class.
    /// </summary>
    public class EmployeePerformanceByTeamRepository : IEmployeePerformanceByTeamRepository
    {
        /// <summary>
        /// Get employee performance by team report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Report Employee Performance By Team Model.</returns>
        public TeamPerformanceModel GetEmployeePerformanceByTeamReport(ICriteria criteria)
        {
            var employeePerformanceByTeamReport = new TeamPerformanceModel();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var reportByProject =
                        multi.Read<EmployeePerformanceByProjectModel, Group, EmployeePerformanceByProjectModel>(
                        (re, gr) =>
                        {
                            re.Group = gr;
                            return re;
                        },
                        "GroupId");
                    employeePerformanceByTeamReport.ReportByProject = reportByProject;
                }
            }

            return employeePerformanceByTeamReport;
        }

        /// <summary>
        /// Gets the redmine personal effort detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedmineTeamEffortViewModel data.</returns>        
        public RedmineTeamEffortViewModel GetRedmineTeamEffort(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var report = new RedmineTeamEffortViewModel();

                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    report.TeamEfforts = GetTeamEfforts(multi);

                    report.MemberProjects = GetMemberProjects(multi);

                    report.YearMonths = multi.Read<RedmineYearMonthModel>();

                    report.MemberSubTotals = GetMemberSubTotals(multi);

                    report.MemberWorkingDays = GetWorkingDays(multi);

                    report.MemberWorkloads = GetMemberWorkloads(multi);

                    report.TotalEfforts = GetTotalEfforts(multi);
                }

                return report;
            }
        }

        /// <summary>
        /// Gets the total efforts.
        /// </summary>
        /// <param name="multi">The multi.</param>
        /// <returns>RedmineTeamTotalEffortModel collection.</returns>
        private static IEnumerable<RedmineTeamTotalEffortModel> GetTotalEfforts(GridReader multi)
        {
            var totalEfforts = multi.Read<RedmineYearMonthModel, RedmineTeamTotalEffortModel, RedmineTeamTotalEffortModel>(
                (y, te) =>
                {
                    te.YearMonth = y;

                    return te;
                },
                "Total");
            return totalEfforts;
        }

        /// <summary>
        /// Gets the member workloads.
        /// </summary>
        /// <param name="multi">The multi.</param>
        /// <returns>RedmineMemberWorkloadModel collection.</returns>
        private static IEnumerable<RedmineMemberWorkloadModel> GetMemberWorkloads(GridReader multi)
        {
            var memberWorkloads =
                multi.Read<RedmineEmployeeModel, RedmineYearMonthModel, RedmineMemberWorkloadModel, RedmineMemberWorkloadModel>(
                (e, y, wl) =>
                {
                    wl.Employee = e;
                    wl.YearMonth = y;

                    return wl;
                },
                "Year,Workload");
            return memberWorkloads;
        }

        /// <summary>
        /// Gets the working days.
        /// </summary>
        /// <param name="multi">The multi.</param>
        /// <returns>RedmineMemberWorkingDayModel collection.</returns>
        private static IEnumerable<RedmineMemberWorkingDayModel> GetWorkingDays(GridReader multi)
        {
            var memberWorkingDays =
                multi.Read<RedmineEmployeeModel, RedmineYearMonthModel, RedmineMemberWorkingDayModel, RedmineMemberWorkingDayModel>(
                (e, y, w) =>
                {
                    w.Employee = e;
                    w.YearMonth = y;

                    return w;
                },
                "Year,WorkingDay");
            return memberWorkingDays;
        }

        /// <summary>
        /// Gets the member sub totals.
        /// </summary>
        /// <param name="multi">The multi.</param>
        /// <returns>RedmineMemberSubTotalModel collection.</returns>
        private static IEnumerable<RedmineMemberSubTotalModel> GetMemberSubTotals(GridReader multi)
        {
            var memberSubTotals =
                multi.Read<RedmineEmployeeModel, RedmineYearMonthModel, RedmineMemberSubTotalModel, RedmineMemberSubTotalModel>(
                (e, y, s) =>
                {
                    s.Employee = e;
                    s.YearMonth = y;

                    return s;
                },
                "Year,SpentTime");
            return memberSubTotals;
        }

        /// <summary>
        /// Gets the member projects.
        /// </summary>
        /// <param name="multi">The multi.</param>
        /// <returns>RedmineMemberProjectModel collection.</returns>
        private static IEnumerable<RedmineMemberProjectModel> GetMemberProjects(GridReader multi)
        {
            var memberProjects =
                multi.Read<RedmineEmployeeModel, RedmineProjectModel, RedmineMemberProjectModel, RedmineMemberProjectModel>(
                (e, p, m) =>
                {
                    if (m == null)
                    {
                        m = new RedmineMemberProjectModel();
                    }

                    m.Employee = e;
                    m.Project = p;

                    return m;
                },
                "ProjectId,SubTotal");
            return memberProjects;
        }

        /// <summary>
        /// Gets the team efforts.
        /// </summary>
        /// <param name="multi">The multi.</param>
        /// <returns>RedmineTeamEffortModel collection.</returns>
        private static IEnumerable<RedmineTeamEffortModel> GetTeamEfforts(GridReader multi)
        {
            var teamEfforts =
                multi.Read<RedmineEmployeeModel, RedmineProjectModel, RedmineYearMonthModel, RedmineTeamEffortModel, RedmineTeamEffortModel>(
                (e, p, y, te) =>
                {
                    te.Employee = e;
                    te.Project = p;
                    te.YearMonth = y;
                    return te;
                },
                "ProjectId,Year,SpentTime");

            return teamEfforts;
        }
    }
}
