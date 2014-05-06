// --------------------------------------------------------------------------
// <copyright file="IProjectService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.20">Created</change>
// </history>

namespace NexEPA.Project.Services.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Models;

    /// <summary>
    /// Interface IProjectService.
    /// </summary>
    public interface IProjectService
    {
        ProjectModel GetById(GetProjectByIdCriteria criteria);

        dynamic GetAllProject(GetAllProjectCriteria criteria);

        CommandResult InsertProject(AddProjectCriteria criteria);

        CommandResult UpdateProject(UpdateProjectCriteria criteria);

        IEnumerable<MilestoneModel> GetMilestoneByProjectId(GetMilestoneByProjectIdCriteria criteria);

        CommandResult UpdateMilestone(UpdateMilestoneCriteria criteria);

        CommandResult InsertMilestone(AddMilestoneCriteria criteria);

        CommandResult DeleteMilestone(DeleteMilestoneCriteria criteria);
    }
}
