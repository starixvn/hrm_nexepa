// --------------------------------------------------------------------------
// <copyright file="EmployeePerformanceTendencyRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// EmployeePerformanceTendencyRepository class.
    /// </summary>
    public class EmployeePerformanceTendencyRepository : IEmployeePerformanceTendencyRepository
    {
        /// <summary>
        /// Get employee performance tendency report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Report Employee Performance Tendency Model.</returns>
        public EmployeePerformanceModel GetEmployeePerformanceTendancyReport(ICriteria criteria)
        {
            var employeePerformanceTendencyReport = new EmployeePerformanceModel();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var employeeGeneralInformationList = (List<EmployeeModel>)multi.Read<EmployeeModel, Group, JobTitle, EmployeeModel>(
                        (emp, gr, job) =>
                        {
                            emp.Group = gr;
                            emp.JobTitle = job;
                            return emp;
                        },
                        "GroupId,JobTitleId");
                    EmployeeModel employeeGeneralInformation = employeeGeneralInformationList != null && employeeGeneralInformationList.Count > 0
                                        ? employeeGeneralInformationList.FirstOrDefault() : new EmployeeModel();
                    employeePerformanceTendencyReport.EmployeeInformation = employeeGeneralInformation;

                    IEnumerable<HistoryProjectModel> historyProjects =
                        multi.Read<HistoryProjectModel, ProjectModel, EmployeeModel, ReportMilestoneModel, HistoryProjectModel>(
                        (proHis, pro, eva, mi) =>
                        {
                            proHis.Project = pro;
                            proHis.Evaluator = eva;
                            proHis.Milestone = mi;
                            return proHis;
                        },
                        "ProjectId,EmployeeId,MilestoneId");
                    employeePerformanceTendencyReport.RawHistoryProjects = historyProjects;
                }
            }

            return employeePerformanceTendencyReport;
        }

        /// <summary>
        /// Gets the redmine project effort report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedminePersonalEffortViewModel class.</returns>
        public RedminePersonalEffortViewModel GetRedmineProjectEffort(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var report = new RedminePersonalEffortViewModel();

                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var personalEfforts = multi.Read<RedminePersonalEffortModel>();
                    var projects = multi.Read<RedmineProjectModel>();
                    var months = multi.Read<RedmineProjectMonthModel>();

                    report.PersonalEfforts = personalEfforts;
                    report.Projects = projects;
                    report.ProjectMonths = months;
                }

                return report;
            }
        }

        /// <summary>
        /// Gets the redmine personal effort detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedminePersonalEffortDetailViewModel class.</returns>
        public RedminePersonalEffortDetailViewModel GetRedminePersonalEffortDetail(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var report = new RedminePersonalEffortDetailViewModel();

                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var projectTrackers = multi.Read<RedmineProjectTrackerModel, RedmineTrackerModel, RedmineProjectTrackerModel>(
                        (pt, t) =>
                        {
                            pt.Tracker = t;
                            return pt;
                        },
                        "TrackerId");
                    var projectEffortDetails = multi.Read<RedminePersonalEffortDetailModel>();
                    var trackerMonths = multi.Read<RedmineTrackerMonthModel, RedmineTrackerYearMonthModel, RedmineTrackerMonthModel>(
                        (t, ty) =>
                        {
                            t.YearMonth = ty;
                            return t;
                        },
                        "Year");
                    var workloadByProjectMonths = multi.Read<RedmineWorkloadByProjectMonthModel>();
                    var workloadByProjects = multi.Read<RedmineWorkloadByProjectModel>();
                    var workloadByMonths = multi.Read<RedmineWorkloadByMonthModel>();

                    report.ProjectTrackers = projectTrackers;
                    report.PersonalEffortDetails = projectEffortDetails;
                    report.TrackerMonths = trackerMonths;
                    report.WorkloadByProjectMonths = workloadByProjectMonths;
                    report.WorkloadByProjects = workloadByProjects;
                    report.WorkloadByMonths = workloadByMonths;
                }

                return report;
            }
        }
    }
}
