// --------------------------------------------------------------------------
// <copyright file="TeamEPAReportRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Implement
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Report.Models.TeamEPAReport;
    using NexEPA.Report.Repositories.Interface;

    /// <summary>
    /// TeamEPAReportRepository class.
    /// </summary>
    public class TeamEPAReportRepository : ITeamEPAReportRepository
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
    }
}
