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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Core.Utilities;
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

        public IList<RedmineProjectModel> GetRedmineProject(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    IList<RedmineProjectModel> projects = multi.Read<RedmineProjectModel>().ToList();
                    IList<RedmineProjectModel> parentProjects = multi.Read<RedmineProjectModel>().ToList();
                    IList<CommonEmployeeModel> projectManagers = multi.Read<CommonEmployeeModel>().ToList();

                    projects = GroupRedmineProjects(parentProjects, projects, projectManagers);

                    return projects;
                }
            }
        }

        public CommandResult SyncProjectsFromRedmine(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                Hashtable output = db.GetOuputParams();

                var result = new CommandResult();

                if (output != null)
                {
                    result.Code = CommonHelper.ConvertInt(output["errcode"]);
                    result.Message = string.Format("{0}", output["errmsg"]);
                }

                return result;
            }
        }

        private IList<RedmineProjectModel> GroupRedmineProjects(
            IList<RedmineProjectModel> parentProjects,
            IList<RedmineProjectModel> projects,
            IList<CommonEmployeeModel> projectManagers)
        {
            var groupedProjects = parentProjects.Where(x => !x.ParentId.HasValue)
                .Select(x =>
                {
                    List<RedmineProjectModel> subProjects = GetRecursiveParentProjects(parentProjects, projects, projectManagers, x.Id);

                    x.SubProjects = subProjects;
                    x.ProjectManagers = GetProjectManagers(projectManagers, x.Id);

                    return x;
                })
                .OrderBy(x => x.Name)
                .ToList();

            return groupedProjects;
        }

        private List<RedmineProjectModel> GetRecursiveParentProjects(
            IList<RedmineProjectModel> parentProjects,
            IList<RedmineProjectModel> projects,
            IList<CommonEmployeeModel> projectManagers,
            int parentId)
        {
            List<RedmineProjectModel> subProjects = parentProjects.Where(sub => sub.ParentId == parentId)
                .Union(projects.Where(sub => sub.ParentId == parentId), new RedmineProjectComparer())
                .Select(p =>
                {
                    List<RedmineProjectModel> childProjects = GetRecursiveParentProjects(parentProjects, projects, projectManagers, p.Id);

                    p.SubProjects = childProjects;
                    p.ProjectManagers = GetProjectManagers(projectManagers, p.Id);

                    return p;
                }).ToList();

            return subProjects;
        }

        private List<CommonEmployeeModel> GetProjectManagers(IList<CommonEmployeeModel> projectManagers, int projectId)
        {
            List<CommonEmployeeModel> matchedProjectManagers = projectManagers.Where(pm => pm.ProjectId == projectId).ToList();

            return matchedProjectManagers;
        }

        private class RedmineProjectComparer : IEqualityComparer<RedmineProjectModel>
        {
            public bool Equals(RedmineProjectModel x, RedmineProjectModel y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(RedmineProjectModel obj)
            {
                return obj.Id.GetHashCode();
            }
        }
    }
}
