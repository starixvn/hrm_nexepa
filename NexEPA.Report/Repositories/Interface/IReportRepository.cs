// -----------------------------------------------------------------------
// <copyright file="IReportRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using Fanex.NAP.Framework.Data;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;
    using NexEPA.Report.Models.EmployeePerformanceTendency;

    /// <summary>
    /// The interface of Report Repository.
    /// </summary>
    public interface IReportRepository
    {
        DashboardModel GetGeneralInformationOfCurrentEmployee(ICriteria criteria);

        ReportEmployeePerformanceTendencyModel GetEmployeePerformanceTendancyReport(ICriteria criteria);

        ReportEmployeePerformanceByTeamModel GetEmployeePerformanceByTeamReport(ICriteria criteria);
    }
}
