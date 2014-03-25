// --------------------------------------------------------------------------
// <copyright file="IProjectRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Models;

    /// <summary>
    /// Interface Project Repository.
    /// </summary>
    public interface IProjectRepository
    {
        IEnumerable<ProjectModel> GetAllProject(ICriteria criteria);
    }
}
