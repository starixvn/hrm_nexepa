// -----------------------------------------------------------------------
// <copyright file="IReportService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Services.Interface
{
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.ViewModels;
    using System.Collections.Generic;

    /// <summary>
    /// The interface of Report Service.
    /// </summary>
    public interface IReportService
    {
        DashboardModel GetGeneralInformationOfCurrentEmployee(GetGeneralInformationOfCurrentEmployeeCriteria criteria);

        List<HistoryProjectViewModel> GetProjectHistory(List<HistoryProjectModel> rawHistoryProjects);
    }
}
