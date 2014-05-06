// -----------------------------------------------------------------------
// <copyright file="ReportService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Core.Service.Impl;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ReportService : BaseService, IReportService
    {
        private IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository = null)
        {
            _reportRepository = reportRepository ?? new ReportRepository();
        }

        /// <summary>
        /// Get general information of current employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List ProjectHistoryViewModel.</returns>
        public List<object> GetGeneralInformationOfCurrentEmployee(
            GetGeneralInformationOfCurrentEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var generalInformation = new List<object>();

            if (criteria.IsValid())
            {
                List<object> rawsGeneralInformation = _reportRepository.GetGeneralInformationOfCurrentEmployee(criteria);

                if (rawsGeneralInformation != null && rawsGeneralInformation.Count > 3)
                {
                    // Get General Information
                    generalInformation.Add(rawsGeneralInformation[0]);

                    // Get Open Project
                    generalInformation.Add(rawsGeneralInformation[1]);

                    // Get History Project
                    List<HistoryProjectViewModel> projectHistoryList = GetProjectHistory((List<HistoryProjectModel>)rawsGeneralInformation[2]);
                    generalInformation.Add(projectHistoryList);

                    // Get KPI Information
                    generalInformation.Add(rawsGeneralInformation[3]);
                }
            }

            return generalInformation;
        }

        private List<HistoryProjectViewModel> GetProjectHistory(List<HistoryProjectModel> rawsProjectHistory)
        {
            var projectHistory = new List<HistoryProjectViewModel>();

            if (rawsProjectHistory != null && rawsProjectHistory.Count > 0)
            {
            }

            return projectHistory;
        }
    }
}
