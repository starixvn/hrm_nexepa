// --------------------------------------------------------------------------
// <copyright file="ITeamEPAReportService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Services.Interface
{
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.TeamEPAReport;

    /// <summary>
    /// ITeamEPAReportService interface.
    /// </summary>
    public interface ITeamEPAReportService
    {
        TeamPerformanceModel GetEmployeePerformanceByTeamReport(GetEmployeePerformanceByTeamCriteria criteria);
    }
}
