// --------------------------------------------------------------------------
// <copyright file="ProjectService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.20">Created</change>
// </history>

namespace NexEPA.Project.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Models;
    using NexEPA.Project.Repositories.Implement;
    using NexEPA.Project.Repositories.Interface;
    using NexEPA.Project.Services.Interface;

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

        /// <summary>
        /// Get Project By id.
        /// </summary>
        /// <param name="criteria">Get Project By Id Criteria.</param>
        /// <returns>Project Model.</returns>
        public ProjectModel GetById(GetProjectByIdCriteria criteria)
        {
            ProjectModel result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.GetDataEntity<ProjectModel>(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get all project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>List Project Model.</returns>
        public List<ProjectInformationModel> GetAllProject(GetAllProjectCriteria criteria)
        {
            List<ProjectInformationModel> lstProject = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                lstProject = _projectRepository.GetAllProject(criteria);
            }

            return lstProject;
        }

        /// <summary>
        /// Insert Project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult InsertProject(AddProjectCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        /// <summary>
        /// Update Project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateProject(UpdateProjectCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Update Project Status.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateProjectStatus(UpdateProjectStatusCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get Milestone By Project Id.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>List Milestone.</returns>
        public IEnumerable<MilestoneModel> GetMilestoneByProjectId(GetMilestoneByProjectIdCriteria criteria)
        {
            IEnumerable<MilestoneModel> lstMilestone = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                lstMilestone = this.GetData<MilestoneModel>(criteria);
            }

            return lstMilestone;
        }

        /// <summary>
        /// Update Milestone.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateMilestone(UpdateMilestoneCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Insert Milestone.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult InsertMilestone(AddMilestoneCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        /// <summary>
        /// Delete Milestone.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult DeleteMilestone(DeleteMilestoneCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }

        /// <summary>
        /// Delete Project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult DeleteProject(DeleteProjectCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }
    }
}
