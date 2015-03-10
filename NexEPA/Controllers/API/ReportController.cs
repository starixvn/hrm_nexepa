// -----------------------------------------------------------------------
// <copyright file="ReportController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.27">Created</change>
//   <change who="Larry.Tran" date="2015.2.5">Modified</change>
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
    using NexEPA.Report.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Report Controller.
    /// </summary>
    public class ReportController : BaseController
    {
        private IEmployeePerformanceByTeamService _teamService;
        private IEmployeePerformanceTendencyService _tendencyService;

        public ReportController()
            : this(null, null)
        {
        }

        public ReportController(IEmployeePerformanceByTeamService teamService, IEmployeePerformanceTendencyService tendencyService)
        {
            _teamService = teamService ?? new EmployeePerformanceByTeamService();
            _tendencyService = tendencyService ?? new EmployeePerformanceTendencyService();
        }

        /// <summary>
        /// Gets the employee performance tendency report.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <returns>EmployeePerformanceModel data.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.PersonReport, Policy = Policy.Read)]
        public EmployeePerformanceModel GetEmployeePerformanceTendencyReport(int employeeId, DateTime fromDate, DateTime toDate)
        {
            var criteria = new GetEmployeePerformanceTendencyReportCriteria
            {
                EmployeeId = employeeId,
                ActorUserId = this.CurrentUser.UserId,
                FromDate = fromDate,
                ToDate = toDate
            };

            return _tendencyService.GetEmployeePerformanceTendancyReport(criteria);
        }

        /// <summary>
        /// Gets the employee performance by team report.
        /// </summary>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="nickNames">The nick names.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>The employee performance by team report.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEPAReport, Policy = Policy.Read)]
        public TeamPerformanceModel GetEmployeePerformanceByTeamReport(
            DateTime fromDate,
            DateTime toDate,
            string nickNames = null,
            int? groupId = null,
            int? projectId = null)
        {
            var criteria = new GetEmployeePerformanceByTeamCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                FromDate = fromDate,
                ToDate = toDate,
                NickNames = nickNames,
                GroupId = groupId,
                ProjectId = projectId
            };

            return _teamService.GetEmployeePerformanceByTeamReport(criteria);
        }

        /// <summary>
        /// Checks the right to view team report.
        /// ApiAuthorize Attribute handles this.
        /// </summary>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEPAReport, Policy = Policy.Read)]
        public void CheckTheRightToViewTeamReport()
        {
            return;
        }

        /// <summary>
        /// Gets the redmine team effort.
        /// </summary>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="nickNames">The nick names is a collection of nick name separated by comma.</param>
        /// <returns>RedmineTeamEffortViewModel data.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEPAReport, Policy = Policy.Read)]
        public RedmineTeamEffortViewModel GetRedmineTeamEffort(
            DateTime fromDate,
            DateTime toDate,
            string nickNames)
        {
            var criteria = new GetRedmineTeamEffortCriteria
            {
                FromDate = fromDate,
                ToDate = toDate,
                NickName = nickNames
            };

            return _teamService.GetRedmineTeamEffort(criteria);
        }

        /// <summary>
        /// Gets the redmine project effort report.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <returns>RedminePersonalEffortViewModel class.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.PersonReport, Policy = Policy.Read)]
        public RedminePersonalEffortViewModel GetRedminePersonalEffort(int employeeId, DateTime fromDate, DateTime toDate)
        {
            var criteria = new GetRedminePersonalEffortCriteria
            {
                EmployeeId = employeeId,
                FromDate = fromDate,
                ToDate = toDate
            };

            return _tendencyService.GetRedminePersonalEffort(criteria);
        }

        /// <summary>
        /// Gets the redmine project effort detail.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <returns>RedminePersonalEffortDetailViewModel class.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.PersonReport, Policy = Policy.Read)]
        public RedminePersonalEffortDetailViewModel GetRedminePersonalEffortDetail(int employeeId, DateTime fromDate, DateTime toDate)
        {
            var criteria = new GetRedminePersonalEffortDetailCriteria
            {
                EmployeeId = employeeId,
                FromDate = fromDate,
                ToDate = toDate
            };

            return _tendencyService.GetRedminePersonalEffortDetail(criteria);
        }
    }
}