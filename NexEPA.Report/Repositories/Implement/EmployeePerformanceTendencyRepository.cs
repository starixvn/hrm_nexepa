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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using Fanex.Data;
    using NexEPA.Employee.Models;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Project.Models;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Report.Repositories.Interface;

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
    }
}
