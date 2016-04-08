// --------------------------------------------------------------------------
// <copyright file="IProjectRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Project.Models;

    /// <summary>
    /// Interface Project Repository.
    /// </summary>
    public interface IProjectRepository
    {
        List<ProjectInformationModel> GetAllProject(ICriteria criteria);

        IList<RedmineProjectModel> GetRedmineProject(ICriteria criteria);
    }
}
