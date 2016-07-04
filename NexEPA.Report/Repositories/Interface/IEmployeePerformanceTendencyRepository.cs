// --------------------------------------------------------------------------
// <copyright file="IEmployeePerformanceTendencyRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using Fanex.Data.Repository;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// IEmployeePerformanceTendencyRepository interface.
    /// </summary>
    public interface IEmployeePerformanceTendencyRepository
    {
        EmployeePerformanceModel GetEmployeePerformanceTendancyReport(ICriteria criteria);

        RedminePersonalEffortViewModel GetRedminePersonalEffort(ICriteria criteria);

        RedminePersonalEffortDetailViewModel GetRedminePersonalEffortDetail(ICriteria criteria);
    }
}
