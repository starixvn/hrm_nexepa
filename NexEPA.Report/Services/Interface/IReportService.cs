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
    using System.Collections.Generic;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;

    /// <summary>
    /// The interface of Report Service.
    /// </summary>
    public interface IReportService
    {
        List<object> GetGeneralInformationOfCurrentEmployee(GetGeneralInformationOfCurrentEmployeeCriteria criteria);

        List<object> GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria);

        IEnumerable<ReportEmployeeByTeamModel> GetEmployeePerformanceByTeamReport(GetEmployeePerformanceByTeamCriteria criteria);
    }
}
