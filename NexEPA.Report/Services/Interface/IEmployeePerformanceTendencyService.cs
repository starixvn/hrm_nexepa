// --------------------------------------------------------------------------
// <copyright file="IEmployeePerformanceTendencyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Services.Interface
{
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// IEmployeePerformanceTendencyService interface.
    /// </summary>
    public interface IEmployeePerformanceTendencyService
    {
        EmployeePerformanceModel GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria);

        RedminePersonalEffortViewModel GetRedmineProjectEffort(GetRedminePersonalEffortCriteria criteria);

        RedminePersonalEffortDetailViewModel GetRedmineProjectEffortDetail(GetRedminePersonalEffortDetailCriteria criteria);
    }
}
