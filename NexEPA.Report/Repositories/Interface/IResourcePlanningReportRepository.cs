// --------------------------------------------------------------------------
// <copyright file="IResourcePlanningReportRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using Fanex.Data.Repository;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// IResourcePlanningReportRepository interface.
    /// </summary>
    public interface IResourcePlanningReportRepository
    {
        RedmineResourcePlanningViewModel GetRedmineResourcePlanning(ICriteria criteria);
    }
}
