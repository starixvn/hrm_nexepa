// --------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Models;
    using NexEPA.Project.Repositories.Interface;

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
        public List<ProjectInformationModel> GetAllProject(ICriteria criteria)
        {
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

                var groupProject = entity.GroupBy(
                    x => new
                {
                    x.ProjectId,
                    x.Name,
                    x.ProjectManager,
                    x.FullName,
                    x.Members,
                    x.StartDate,
                    x.EndDate,
                    x.Status,
                    x.NickName,
                    x.Permission,
                    x.TotalEPA,
                    x.TotalOpenEPA
                },
                x => x.Milestone).Select(y => new ProjectInformationModel
                {
                    Name = y.Key.Name,
                    ProjectId = y.Key.ProjectId,
                    ProjectManager = y.Key.ProjectManager.Value,
                    FullName = y.Key.FullName,
                    NickName = y.Key.NickName,
                    Members = y.Key.Members.Value,
                    StartDate = y.Key.StartDate.Value,
                    EndDate = y.Key.EndDate,
                    Status = y.Key.Status.Value,
                    Permission = y.Key.Permission,
                    Milestones = y.ToList(),
                    TotalEPA = y.Key.TotalEPA,
                    TotalOpenEPA = y.Key.TotalOpenEPA
                }).ToList();

                return groupProject;
            }
        }
    }
}
