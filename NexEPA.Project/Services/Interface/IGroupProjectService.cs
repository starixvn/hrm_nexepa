// -----------------------------------------------------------------------
// <copyright file="IGroupProjectService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.14">Created</change>
// </history>

namespace NexEPA.Project.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Project.Criteria.GroupProject;
    using NexEPA.Project.Models.GroupProject;

    /// <summary>
    /// Interface group project service.
    /// </summary>
    public interface IGroupProjectService
    {
        List<GroupProjectModel> GetAllGroupProject(GetAllGroupProjectCriteria criteria);

        CommandResult InsertGroupProject(InsertGroupProjectCriteria criteria);

        CommandResult UpdateGroupProject(UpdateGroupProjectCriteria criteria);

        CommandResult DeleteGroupProject(DeleteGroupProjectCriteria criteria);
    }
}