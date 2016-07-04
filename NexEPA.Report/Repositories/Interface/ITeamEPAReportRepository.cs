// --------------------------------------------------------------------------
// <copyright file="ITeamEPAReportRepository.cs" company="">
// Copyright (c) . All rights reserved.
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
