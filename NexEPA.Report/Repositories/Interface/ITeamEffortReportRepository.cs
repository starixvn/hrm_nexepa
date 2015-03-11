// --------------------------------------------------------------------------
// <copyright file="ITeamEffortReportRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.10">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using Fanex.Data.Repository;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// ITeamEffortReportRepository interface.
    /// </summary>
    public interface ITeamEffortReportRepository
    {
        RedmineTeamEffortViewModel GetRedmineTeamEffort(ICriteria criteria);
    }
}
