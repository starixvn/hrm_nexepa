// --------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Repositories.Impl
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Models;
    using NexEPA.Project.Repositories.Interfaces;

    /// <summary>
    /// Project Repository.
    /// </summary>
    public class ProjectRepository : IProjectRepository
    {
        /// <summary>
        /// Get all projects.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Enumerable of Project Model.</returns>
        public IEnumerable<ProjectModel> GetAllProject(ICriteria criteria)
        {
            ProjectModel project = new ProjectModel();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var entity = db.Query<ProjectModel, MilestoneModel, ProjectModel>(
                    (p, m) =>
                    {
                        p.Milestone = m;
                        return p;
                    },
                    "MilestoneId",
                    criteria.GetSpParams());

                return entity;
            }
        }
    }
}
