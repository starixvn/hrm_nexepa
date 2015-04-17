// -----------------------------------------------------------------------
// <copyright file="IGroupProjectRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.15">Created</change>
// </history>

namespace NexEPA.Project.Repositories.Interface
{
    using System.Collections.Generic;
    using NexEPA.Project.Criteria.GroupProject;
    using NexEPA.Project.Models.GroupProject;

    /// <summary>
    /// Interface group project repository.
    /// </summary>
    public interface IGroupProjectRepository
    {
        List<GroupProjectModel> GetAllGroupProject(GetAllGroupProjectCriteria criteria);

        List<GroupProjectDetailModel> GetAllProject(GetAllProjectCriteria criteria);
    }
}