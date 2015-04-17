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
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria.GroupProject;
    using NexEPA.Project.Models.GroupProject;
    using NexEPA.Project.Repositories.Implement;
    using NexEPA.Project.Repositories.Interface;
    using NexEPA.Project.Services.Interface;

    /// <summary>
    /// Implement of interface group project service.
    /// </summary>
    public class GroupProjectService : BaseService, IGroupProjectService
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

        /// <summary>
        /// Insert group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public CommandResult InsertGroupProject(InsertGroupProjectCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var result = new CommandResult();
            if (criteria.IsValid())
            {
                result = Repository.Insert(criteria, true);
            }

            return result;
        }

        /// <summary>
        /// Update group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public CommandResult UpdateGroupProject(UpdateGroupProjectCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var result = new CommandResult();
            if (criteria.IsValid())
            {
                result = Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Delete group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        public CommandResult DeleteGroupProject(DeleteGroupProjectCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var result = new CommandResult();
            if (criteria.IsValid())
            {
                result = Repository.Delete(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get all project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List project.</returns>
        public List<GroupProjectDetailModel> GetAllProject(GetAllProjectCriteria criteria)
        {
            var projects = new List<GroupProjectDetailModel>();
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                projects = _groupProjectRepository.GetAllProject(criteria);
            }

            return projects;
        }
    }
}