// --------------------------------------------------------------------------
// <copyright file="IEmployeePerformanceTendencyService.cs" company="">
// Copyright (c) . All rights reserved.
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

        RedminePersonalEffortViewModel GetRedminePersonalEffort(GetRedminePersonalEffortCriteria criteria);

        RedminePersonalEffortDetailViewModel GetRedminePersonalEffortDetail(GetRedminePersonalEffortDetailCriteria criteria);
    }
}
