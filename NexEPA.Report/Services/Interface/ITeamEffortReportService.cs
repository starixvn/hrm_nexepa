// --------------------------------------------------------------------------
// <copyright file="ITeamEffortReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.10">Created</change>
// </history>

namespace NexEPA.Report.Services.Interface
{
    using NexEPA.Report.Criteria;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// ITeamEffortReportService interface.
    /// </summary>
    public interface ITeamEffortReportService
    {
        RedmineTeamEffortViewModel GetRedmineTeamEffort(GetRedmineTeamEffortCriteria criteria);
    }
}
