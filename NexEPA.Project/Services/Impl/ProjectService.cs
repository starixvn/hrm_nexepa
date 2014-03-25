// --------------------------------------------------------------------------
// <copyright file="ProjectService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.20">Created</change>
// </history>

namespace NexEPA.Project.Services.Impl
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria;
    using NexEPA.Project.Models;
    using NexEPA.Project.Repositories.Impl;
    using NexEPA.Project.Repositories.Interfaces;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Project Service.
    /// </summary>
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository = null)
        {
            this._projectRepository = projectRepository ?? new ProjectRepository();
        }

        public ProjectModel GetById(GetProjectByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                var result = this.GetDataEntity<ProjectModel>(criteria);
                return result;
            }

            return null;
        }

        public IEnumerable<ProjectModel> GetAllProject(GetAllProjectCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                return _projectRepository.GetAllProject(criteria);
            }

            return null;
        }

        public CommandResult InsertProject(AddProjectCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                var result = this._Repository.Insert(criteria);
                return result;
            }

            return null;
        }

        public CommandResult UpdateProject(UpdateProjectCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                var result = this._Repository.Update(criteria);
                return result;
            }

            return null;
        }

        public IEnumerable<MilestoneModel> GetMilestoneByProjectId(GetMilestoneByProjectIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                var result = this.GetData<MilestoneModel>(criteria);
                return result;
            }

            return null;
        }

        public CommandResult UpdateMilestone(UpdateMilestoneCriteria criteria)
        {
             Check.Argument.IsNotNull(criteria, "criteria");
             if (criteria.IsValid())
             {
                 var result = this._Repository.Update(criteria);
                 return result;
             }

             return null;
        }

        public CommandResult InsertMilestone(AddMilestoneCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                var result = this._Repository.Insert(criteria);
                return result;
            }

            return null;
        }

        public CommandResult DeleteMilestone(DeleteMilestoneCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                var result = this._Repository.Delete(criteria);
                return result;
            }

            return null;
        }
    }
}
