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
    using NexEPA.Core.Model.Project;
    using NexEPA.Core.Utilities;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services.Implement;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Project Controller.
    /// </summary>
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly EmployeeServices _employeeService;

        public ProjectController()
            : this(null, null)
        {
        }

        public ProjectController(IProjectService projectService, EmployeeServices employeeService)
        {
            this._projectService = projectService ?? new ProjectService();
            this._employeeService = employeeService ?? new EmployeeServices();
        }

        /// <summary>
        /// Get projects by criteria.
        /// </summary>
        /// <param name="projectManager">Project Manager.</param>
        /// <param name="status">Project Status.</param>
        /// <param name="period">Project Period.</param>
        /// <returns>List Project Model.</returns>
        ////[ApiAuthorize]
        public IEnumerable<ProjectModel> Get(int? projectManager = null, int? status = null, int? period = null)
        {
            DateTime? startDate = null;
            if (period != null)
            {
                startDate = GetPeriodOfTime(period.Value);
            }

            GetAllProjectCriteria criteria = new GetAllProjectCriteria()
                {
                    ProjectManager = projectManager,
                    Status = status,
                    StartDate = startDate,
                    EndDate = DateTime.Now.Date
                };

            IEnumerable<ProjectModel> lstProject = _projectService.GetAllProject(criteria);

            return lstProject;
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
                    StartDate = TimeZone.CurrentTimeZone.ToLocalTime(project.StartDate.Value),
                    EndDate = TimeZone.CurrentTimeZone.ToLocalTime(project.EndDate.Value),
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

        [HttpGet]
        public IEnumerable<dynamic> RenderEmployeeSelectList(int? roleId = null)
        {
            GetEmployeeByRoleCriteria criteria = new GetEmployeeByRoleCriteria() { RoleId = roleId };
            IEnumerable<EmployeeInfo> employees = _employeeService.GetData<EmployeeInfo>(criteria);
            var selectListEmployee = employees.Select(x => new { x.FullName, x.EmployeeId });

            return selectListEmployee;
        }

        /// <summary>
        /// Render Project Status Select List.
        /// </summary>
        /// <returns>Array of Project Status.</returns>
        [HttpGet]
        public IEnumerable<SelectListModel> RenderProjectStatusSelectList()
        {
            var data = EnumStatus.GetAllStatus();
            return data;
        }

        /// <summary>
        /// Project period filter select list.
        /// </summary>
        /// <returns>Select list data.</returns>
        [HttpGet]
        public Array RenderProjectPeriodSelectList()
        {
            Array statusList = new[]
                    {
                        new SelectListModel() { Id = 1, Name = "This month" },
                        new SelectListModel() { Id = 2, Name = "Last 3 months" },
                        new SelectListModel() { Id = 3, Name = "Last 6 months" },
                        new SelectListModel() { Id = 4, Name = "Last 12 months" }
                    };

            return statusList;
        }

        [HttpGet]
        public ProjectModel GetProjectById(int projectId)
        {
            GetProjectByIdCriteria criteria = new GetProjectByIdCriteria { ProjectId = projectId };
            ProjectModel project = _projectService.GetById(criteria);

            return project;
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }

        /// <summary>
        /// Get Period of time.
        /// </summary>
        /// <param name="period">Period from filter.</param>
        /// <returns>Date time.</returns>
        private DateTime GetPeriodOfTime(int period)
        {
            DateTime startDate = new DateTime();
            var now = DateTime.Now;
            switch (period)
            {
                case 1:
                    startDate = new DateTime(now.Year, now.Month, 1);
                    break;
                case 2:
                    startDate = DateTime.Now.AddMonths(-3).Date;
                    break;
                case 3:
                    startDate = DateTime.Now.AddMonths(-6).Date;
                    break;
                case 4:
                    startDate = DateTime.Now.AddMonths(-12).Date;
                    break;
                default:
                    startDate = DateTime.Now.Date;
                    break;
            }

            return startDate;
        }
    }
}
