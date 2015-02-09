// --------------------------------------------------------------------------
// <copyright file="EmployeePerformanceTendencyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Core.Utilities;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// EmployeePerformanceTendencyService class.
    /// </summary>
    public class EmployeePerformanceTendencyService : BaseService, IEmployeePerformanceTendencyService
    {
        private IEmployeePerformanceTendencyRepository _repository;
        private IReportService _reportService;

        public EmployeePerformanceTendencyService()
            : this(null, null)
        {
        }

        public EmployeePerformanceTendencyService(
            IEmployeePerformanceTendencyRepository repository,
            IReportService reportService)
        {
            _repository = repository ?? new EmployeePerformanceTendencyRepository();
            _reportService = reportService ?? new ReportService();
        }

        /// <summary>
        /// Get employee performance tendency report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The ReportEmployeePerformanceTendencyModel.</returns>
        public EmployeePerformanceModel GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformanceReport = new EmployeePerformanceModel();

            if (criteria.IsValid())
            {
                EmployeePerformanceModel rawsEmployeePerformanceReport = _repository.GetEmployeePerformanceTendancyReport(criteria);

                if (rawsEmployeePerformanceReport != null)
                {
                    string gravatarString = string.Empty;
                    employeePerformanceReport.EmployeeInformation = rawsEmployeePerformanceReport.EmployeeInformation;

                    var userEmail = employeePerformanceReport.EmployeeInformation.Email;
                    if (userEmail != null)
                    {
                        gravatarString = GravatarImage.HashEmailForGravatar(userEmail);
                    }

                    employeePerformanceReport.EmployeeInformation.Gravatar = gravatarString;

                    var projectHistoryList = _reportService.GetProjectHistory((List<HistoryProjectModel>)rawsEmployeePerformanceReport.RawHistoryProjects);
                    employeePerformanceReport.HistoryProjects = projectHistoryList;
                }
            }

            return employeePerformanceReport;
        }

        /// <summary>
        /// Gets the redmine project effort report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedmineProjectEffortReportViewModel class.</returns>
        public RedmineProjectEffortReportViewModel GetRedmineProjectEffortReport(GetRedmineProjectEffortReportCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var report = new RedmineProjectEffortReportViewModel();

            if (criteria.IsValid())
            {
                report = _repository.GetRedmineProjectEffortReport(criteria);

                if (report.Projects != null)
                {
                    report.GrandTotalSpentTime = report.Projects.Sum(p => p.TotalSpentTimeOfAllMonths);
                }
            }

            return report;
        }
    }
}
