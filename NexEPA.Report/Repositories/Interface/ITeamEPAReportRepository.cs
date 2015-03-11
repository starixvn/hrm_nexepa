// --------------------------------------------------------------------------
// <copyright file="ITeamEPAReportRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using Fanex.Data.Repository;
    using NexEPA.Report.Models.TeamEPAReport;

    /// <summary>
    /// ITeamEPAReportRepository interface.
    /// </summary>
    public interface ITeamEPAReportRepository
    {
        TeamPerformanceModel GetEmployeePerformanceByTeamReport(ICriteria criteria);
    }
}
