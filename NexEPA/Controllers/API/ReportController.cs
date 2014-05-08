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
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Services.Implement;
    using NexEPA.Report.Services.Interface;
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
        public List<object> GetEmployeePerformanceTendencyReport(int employeeId)
        {
            var criteria = new GetEmployeePerformanceTendencyReportCriteria
            {
                EmployeeId = employeeId
            };

            return _reportService.GetEmployeePerformanceTendancyReport(criteria);
        }
    }
}