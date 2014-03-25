// --------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.19">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Criteria;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Project Controller.
    /// </summary>
    public class ProjectController : ApiController
    {
        private readonly IProjectService _projectService;

        public ProjectController()
            : this(null)
        {
        }

        public ProjectController(IProjectService projectService)
        {
            this._projectService = projectService ?? new ProjectService();
        }

        /// <summary>
        /// Get projects by criteria.
        /// </summary>
        /// <param name="projectManager">Project Manager.</param>
        /// <param name="status">Project Status.</param>
        /// <param name="period">Project Period.</param>
        /// <returns>List Project Model.</returns>
        public IEnumerable<ProjectModel> Get(int? projectManager = null, int? status = null, int? period = null)
        {
            GetAllProjectCriteria criteria = new GetAllProjectCriteria();
            if (projectManager != null || status != null || period != null)
            {
                criteria = new GetAllProjectCriteria()
                {
                    ProjectManager = projectManager,
                    Status = status,
                    StartDate = null,
                    EndDate = null
                };
            }

            IEnumerable<ProjectModel> projects = _projectService.GetAllProject(criteria);

            return projects;
        }

        /// <summary>
        /// Insert Project.
        /// </summary>
        /// <param name="project">Project Model.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage Post(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                AddProjectCriteria criteria = new AddProjectCriteria()
                {
                    Name = project.Name,
                    Description = project.Description,
                    ProjectManager = project.ProjectManager,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Status = project.Status,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.Date
                };

                CommandResult result = _projectService.InsertProject(criteria);

                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
            }
        }

        /// <summary>
        /// Update Project.
        /// </summary>
        /// <param name="project">Project Model.</param>
        /// <returns>Http Message Response.</returns>
        public HttpResponseMessage Put(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                UpdateProjectCriteria criteria = new UpdateProjectCriteria
                {
                    ProjectId = project.ProjectId,
                    Name = project.Name,
                    ProjectManager = project.ProjectManager,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Status = project.Status,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now.Date,
                    Description = project.Description
                };

                CommandResult result = _projectService.UpdateProject(criteria);

                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
            }
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
