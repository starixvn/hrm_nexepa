// -----------------------------------------------------------------------
// <copyright file="ReportController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.27">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Services.Implement;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Report Controller.
    /// </summary>
    public class ReportController : BaseController
    {
        private IReportService _reportService;

        public ReportController()
            : this(null)
        {
        }

        public ReportController(IReportService reportService)
        {
            _reportService = reportService ?? new ReportService();
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.PersonReport, Policy = Policy.Read)]
        public EmployeePerformanceModel GetEmployeePerformanceTendencyReport(int employeeId)
        {
            var criteria = new GetEmployeePerformanceTendencyReportCriteria
            {
                EmployeeId = employeeId,
                ActorUserId = this.CurrentUser.UserId
            };

            return _reportService.GetEmployeePerformanceTendancyReport(criteria);
        }

        /// <summary>
        /// Get employee performance by team report.
        /// </summary>
        /// <param name="fromDate">The From Date.</param>
        /// <param name="toDate">The To Date.</param>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="groupId">The group ID.</param>
        /// <param name="projectId">The project ID.</param>
        /// <returns>The Report Employee Performance By Team Model.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamReport, Policy = Policy.Read)]
        public TeamPerformanceModel GetEmployeePerformanceByTeamReport(
            DateTime fromDate,
            DateTime toDate,
            int? employeeId = null,
            int? groupId = null,
            int? projectId = null)
        {
            var criteria = new GetEmployeePerformanceByTeamCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                FromDate = fromDate,
                ToDate = toDate,
                EmployeeId = employeeId,
                GroupId = groupId,
                ProjectId = projectId
            };

            return _reportService.GetEmployeePerformanceByTeamReport(criteria);
        }
    }
}