// -----------------------------------------------------------------------
// <copyright file="GroupProjectRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.15">Created</change>
// </history>

namespace NexEPA.Project.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using NexEPA.Project.Criteria.GroupProject;
    using NexEPA.Project.Models.GroupProject;
    using NexEPA.Project.Repositories.Interface;

    /// <summary>
    /// Implement of interface group project repository.
    /// </summary>
    public class GroupProjectRepository : IGroupProjectRepository
    {
        /// <summary>
        /// Get all group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of Group project model.</returns>
        public List<GroupProjectModel> GetAllGroupProject(GetAllGroupProjectCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var groupProjects = new List<GroupProjectModel>();
                    groupProjects = multi.Read<GroupProjectModel>().ToList();

                    var projects = new List<GroupProjectDetailModel>();
                    projects = multi.Read<GroupProjectDetailModel>().ToList();

                    groupProjects = GetProject(groupProjects, projects);

                    return groupProjects;
                }
            }
        }

        /// <summary>
        /// Group project by group project.
        /// </summary>
        /// <param name="groupProjects">The list of group project.</param>
        /// <param name="projects">The list of project.</param>
        /// <returns>List of group project model.</returns>
        private List<GroupProjectModel> GetProject(List<GroupProjectModel> groupProjects, List<GroupProjectDetailModel> projects)
        {
            foreach (var group in groupProjects)
            {
                foreach (var project in projects)
                {
                    if (project.GroupProjectId == group.GroupProjectId)
                    {
                        group.Projects.Add(project);
                    }
                }
            }

            return groupProjects;
        }
    }
}