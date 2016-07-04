// --------------------------------------------------------------------------
// <copyright file="ITeamEffortReportService.cs" company="">
// Copyright (c) . All rights reserved.
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
