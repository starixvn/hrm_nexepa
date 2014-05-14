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
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
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
        /// <returns>The List Object.</returns>
        public List<object> GetGeneralInformationOfCurrentEmployee(ICriteria criteria)
        {
            var result = new List<object>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var generalInformationList = (List<GeneralInformationOfProjectModel>)multi.Read<GeneralInformationOfProjectModel>();
                    GeneralInformationOfProjectModel generalInformation = generalInformationList != null && generalInformationList.Count > 0 
                                                ? generalInformationList[0] 
                                                : new GeneralInformationOfProjectModel();
                    result.Add(generalInformation);

                    IEnumerable<OpenProjectModel> openProjects = multi.Read<OpenProjectModel, ProjectModel, EmployeeModel, OpenProjectModel>(
                        (opPro, pro, eva) =>
                        {
                            opPro.Project = pro;
                            opPro.Evaluator = eva;
                            return opPro;
                        },
                        "ProjectId,EmployeeId");
                    result.Add(openProjects);

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
                    result.Add(historyProjects);

                    var listKPIs = (List<KPIModel>)multi.Read<KPIModel>();
                    result.Add(listKPIs);
                }
            }

            return result;
        }

        /// <summary>
        /// Get employee performance tendency report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list object.</returns>
        public List<object> GetEmployeePerformanceTendancyReport(ICriteria criteria)
        {
            var result = new List<object>();

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
                                        ? employeeGeneralInformationList[0] : new EmployeeModel();
                    result.Add(employeeGeneralInformation);

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
                    result.Add(historyProjects);
                }
            }

            return result;
        }

        /// <summary>
        /// Get employee performance by team report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list object.</returns>
        public List<object> GetEmployeePerformanceByTeamReport(ICriteria criteria)
        {
            var result = new List<object>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var employeePerformanceByTeamReport = multi.Read<ReportEmployeeByTeamModel, Group, ReportEmployeeByTeamModel>(
                        (re, gr) => 
                        {
                            re.Group = gr;
                            return re;
                        },
                        "GroupId");
                    result.Add(employeePerformanceByTeamReport);
                }
            }

            return result;
        }
    }
}
