// -----------------------------------------------------------------------
// <copyright file="ReportRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
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
    }
}
