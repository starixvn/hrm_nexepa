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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NexEPA.Core.Services;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Criteria;
    using Fanex.Common.Helper;
    using NexEPA.Core.Utilities;
    using NexEPA.Report.ViewModels;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Target.Models;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.Repositories.Implement;

    /// <summary>
    /// EmployeePerformanceTendencyService class.
    /// </summary>
    public class EmployeePerformanceTendencyService : BaseService, IEmployeePerformanceTendencyService
    {
        private IEmployeePerformanceTendencyRepository _repository;
        private IEncodeService _encodeService;
        private IReportService _reportService;

        public EmployeePerformanceTendencyService()
            : this(null, null, null)
        {
        }

        public EmployeePerformanceTendencyService(
            IEmployeePerformanceTendencyRepository repository,
            IReportService reportService,
            IEncodeService encodeService)
        {
            _repository = repository ?? new EmployeePerformanceTendencyRepository();
            _reportService = reportService ?? new ReportService();
            _encodeService = encodeService ?? new EncodeService();
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

                    IEnumerable<HistoryProjectViewModel> projectHistoryList = _reportService.GetProjectHistory((List<HistoryProjectModel>)rawsEmployeePerformanceReport.RawHistoryProjects);
                    employeePerformanceReport.HistoryProjects = projectHistoryList;
                }
            }

            return employeePerformanceReport;
        }

    }
}
