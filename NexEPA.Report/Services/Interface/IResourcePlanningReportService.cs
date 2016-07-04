// --------------------------------------------------------------------------
// <copyright file="IResourcePlanningReportService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Services.Interface
{
    using NexEPA.Report.Criteria;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// IResourcePlanningReportService class.
    /// </summary>
    public interface IResourcePlanningReportService
    {
        RedmineResourcePlanningViewModel GetRedmineResourcePlanning(GetRedmineResourcePlanningCriteria criteria);
    }
}
