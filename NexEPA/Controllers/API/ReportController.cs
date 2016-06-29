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
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Models.TeamEPAReport;
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
        private ITeamEPAReportService _teamEPAService;
        private IEmployeePerformanceTendencyService _tendencyService;
        private ITeamEffortReportService _teamEffortService;
        private IResourcePlanningReportService _resourcePlanningService;

        public ReportController()
            : this(null, null, null, null)
        {
        }

        public ReportController(
            ITeamEPAReportService teamService,
            ITeamEffortReportService teamEffortService,
            IEmployeePerformanceTendencyService tendencyService,
            IResourcePlanningReportService resourcePlanningService)
        {
            _teamEPAService = teamService ?? new TeamEPAReportService();
            _teamEffortService = teamEffortService ?? new TeamEffortReportService();
            _tendencyService = tendencyService ?? new EmployeePerformanceTendencyService();
            _resourcePlanningService = resourcePlanningService ?? new ResourcePlanningReportService();
        }

        #region PersonReport
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
        /// Gets the redmine project effort report.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="isFilterGroup">Selected project or group project.</param>
        /// <param name="projectIds">The list project id.</param>
        /// <param name="groupProjectId">The group project id.</param>
        /// <returns>RedminePersonalEffortViewModel class.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.PersonReport, Policy = Policy.Read)]
        public RedminePersonalEffortViewModel GetRedminePersonalEffort(
            int employeeId,
            DateTime fromDate,
            DateTime toDate,
            bool isFilterGroup = true,
            string projectIds = null,
            int? groupProjectId = null)
        {
            var criteria = new GetRedminePersonalEffortCriteria
            {
                EmployeeId = employeeId,
                FromDate = fromDate,
                ToDate = toDate,
                IsFilterGroup = isFilterGroup,
                ProjectIds = projectIds,
                GroupProjectId = groupProjectId
            };

            return _tendencyService.GetRedminePersonalEffort(criteria);
        }

        /// <summary>
        /// Gets the redmine project effort detail.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="isFilterGroup">Selected project or group project.</param>
        /// <param name="projectIds">The list project id.</param>
        /// <param name="groupProjectId">The group project id.</param>
        /// <returns>RedminePersonalEffortDetailViewModel class.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.PersonReport, Policy = Policy.Read)]
        public RedminePersonalEffortDetailViewModel GetRedminePersonalEffortDetail(
            int employeeId,
            DateTime fromDate,
            DateTime toDate,
            bool isFilterGroup = true,
            string projectIds = null,
            int? groupProjectId = null)
        {
            var criteria = new GetRedminePersonalEffortDetailCriteria
            {
                EmployeeId = employeeId,
                FromDate = fromDate,
                ToDate = toDate,
                IsFilterGroup = isFilterGroup,
                ProjectIds = projectIds,
                GroupProjectId = groupProjectId
            };

            return _tendencyService.GetRedminePersonalEffortDetail(criteria);
        }
        #endregion

        #region TeamEPAReport
        /// <summary>
        /// Gets the employee performance by team report.
        /// </summary>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="employeeIds">The nick names.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>The employee performance by team report.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEPAReport, Policy = Policy.Read)]
        public TeamPerformanceModel GetEmployeePerformanceByTeamReport(
            DateTime fromDate,
            DateTime toDate,
            string employeeIds = null,
            int? groupId = null,
            int? projectId = null)
        {
            var criteria = new GetEmployeePerformanceByTeamCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                FromDate = fromDate,
                ToDate = toDate,
                EmployeeId = employeeIds,
                GroupId = groupId,
                ProjectId = projectId
            };

            return _teamEPAService.GetEmployeePerformanceByTeamReport(criteria);
        }

        /// <summary>
        /// Checks the right to view team epa report.
        /// ApiAuthorize Attribute handles this.
        /// </summary>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEPAReport, Policy = Policy.Read)]
        public void CheckTheRightToViewTeamEPAReport()
        {
            return;
        }
        #endregion

        #region TeamEffortReport
        /// <summary>
        /// Gets the redmine team effort.
        /// </summary>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="nickNames">The nick names.</param>
        /// <param name="isFilterGroup">Selected project or group project.</param>
        /// <param name="projectIds">The project ids.</param>
        /// <param name="groupProjectId">The group project identifier.</param>
        /// <returns>The redmine team efforts.</returns>
        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEffortReport, Policy = Policy.Read)]
        public RedmineTeamEffortViewModel GetRedmineTeamEffort(GetRedmineTeamEffortCriteria criteria)
        {
            var result = new RedmineTeamEffortViewModel();

            if (criteria != null)
            {
                result = _teamEffortService.GetRedmineTeamEffort(criteria);
            }

            return result;
        }

        /// <summary>
        /// Checks the right to view team effort report.
        /// ApiAuthorize Attribute handles this.
        /// </summary>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.TeamEffortReport, Policy = Policy.Read)]
        public void CheckTheRightToViewTeamEffortReport()
        {
            return;
        }
        #endregion

        /// <summary>
        /// Gets the redmine resource planning.
        /// </summary>
        /// <param name="fromDate">The From date.</param>
        /// <param name="toDate">The To date.</param>
        /// <param name="nickNames">The nick names.</param>
        /// <param name="isFilterGroup">Selected project or group project.</param>
        /// <param name="projectIds">The project ids.</param>
        /// <param name="groupProjectId">The group project identifier.</param>
        /// <returns>The redmine resource planning.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ResourcePlanningReport, Policy = Policy.Read)]
        public RedmineResourcePlanningViewModel GetRedmineResourcePlanning(
            DateTime fromDate,
            DateTime toDate,
            string nickNames,
            bool isFilterGroup,
            string projectIds,
            int? groupProjectId)
        {
            var criteria = new GetRedmineResourcePlanningCriteria
            {
                FromDate = fromDate,
                ToDate = toDate,
                NickName = nickNames,
                IsFilterGroup = isFilterGroup,
                ProjectIds = projectIds,
                GroupProjectId = groupProjectId
            };

            return _resourcePlanningService.GetRedmineResourcePlanning(criteria);
        }
    }
}