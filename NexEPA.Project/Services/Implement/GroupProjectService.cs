// -----------------------------------------------------------------------
// <copyright file="GroupProjectService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.15">Created</change>
// </history>

namespace NexEPA.Project.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using NexEPA.Project.Criteria.GroupProject;
    using NexEPA.Project.Models.GroupProject;
    using NexEPA.Project.Repositories.Implement;
    using NexEPA.Project.Repositories.Interface;
    using NexEPA.Project.Services.Interface;

    /// <summary>
    /// Implement of interface group project service.
    /// </summary>
    public class GroupProjectService : IGroupProjectService
    {
        private readonly IGroupProjectRepository _groupProjectRepository;

        public GroupProjectService()
            : this(null)
        {
        }

        public GroupProjectService(IGroupProjectRepository groupProjectRepository)
        {
            _groupProjectRepository = groupProjectRepository ?? new GroupProjectRepository();
        }

        /// <summary>
        /// Get all group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List group project model.</returns>
        public List<GroupProjectModel> GetAllGroupProject(GetAllGroupProjectCriteria criteria)
        {
            var groupProjects = new List<GroupProjectModel>();
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                groupProjects = _groupProjectRepository.GetAllGroupProject(criteria);
            }

            return groupProjects;
        }
    }
}