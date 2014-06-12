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
    using System.Text.RegularExpressions;
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Core.Utilities;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services.Implement;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Implement;
    using NexEPA.Project.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// Project Controller.
    /// </summary>
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IEmployeeService _employeeService;
        private readonly ICommonService _commonService;

        public ProjectController()
            : this(null, null, null)
        {
        }

        public ProjectController(IProjectService projectService, IEmployeeService employeeService, ICommonService commonService)
        {
            this._projectService = projectService ?? new ProjectService();
            this._employeeService = employeeService ?? new EmployeeService();
            this._commonService = commonService ?? new CommonService();
        }

        /// <summary>
        /// Get projects by criteria.
        /// </summary>
        /// <param name="projectManager">Project Manager.</param>
        /// <param name="status">Project Status.</param>
        /// <param name="period">Project Period.</param>
        /// <returns>List Project Model.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Read)]
        public List<ProjectInformationModel> Get(int? projectManager = null, int? status = null, int? period = null)
        {
            List<ProjectInformationModel> projects = null;
            DateTime? startDate = null;
            if (period != null)
            {
                startDate = GetPeriodOfTime(period.Value);
            }

            var getAllProjectCriteria = new GetAllProjectCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                ProjectManager = projectManager,
                Status = status,
                StartDate = startDate,
                EndDate = DateTime.Now
            };
            projects = _projectService.GetAllProject(getAllProjectCriteria);

            return projects;
        }

        /// <summary>
        /// Insert Project.
        /// </summary>
        /// <param name="project">Project Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage AddProject(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                var addProjectCriteria = new AddProjectCriteria
                {
                    Name = project.Name,
                    Description = project.Description,
                    ProjectManager = project.ProjectManager,
                    StartDate = project.StartDate.Value,
                    EndDate = project.EndDate,
                    Status = project.Status,
                    CreatedBy = this.CurrentUser.UserId,
                    CreatedDate = DateTime.Now
                };

                CommandResult result = _projectService.InsertProject(addProjectCriteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
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
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage Put(ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                var updateProjectCriteria = new UpdateProjectCriteria
                {
                    ProjectId = project.ProjectId,
                    Name = project.Name,
                    ProjectManager = project.ProjectManager,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Status = project.Status,
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now.Date,
                    Description = project.Description
                };

                CommandResult result = _projectService.UpdateProject(updateProjectCriteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
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
        /// Method update project status.
        /// </summary>
        /// <param name="model">Project Information Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage UpdateProjectStatus(ProjectInformationModel model)
        {
            var updateProjectStatusCriteria = new UpdateProjectStatusCriteria
            {
                Status = model.Status,
                ProjectId = model.ProjectId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = _projectService.UpdateProjectStatus(updateProjectStatusCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete Project.
        /// </summary>
        /// <param name="projectId">Project Id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage DeleteProject(int projectId)
        {
            var deleteProjectCriteria = new DeleteProjectCriteria
            {
                ProjectId = projectId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = _projectService.DeleteProject(deleteProjectCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Read)]
        public ProjectModel GetProjectById(int projectId)
        {
            var getProjectByIdCriteria = new GetProjectByIdCriteria
            {
                ProjectId = projectId,
                ActorUserId = this.CurrentUser.UserId
            };
            ProjectModel project = _projectService.GetById(getProjectByIdCriteria);

            return project;
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetPermission()
        {
            int policy = _commonService.GetPermission(EPAFunctionKeys.ManageProject);
            return policy;
        }

        /// <summary>
        /// Render Employee Select list.
        /// </summary>
        /// <param name="roleId">The roleID.</param>
        /// <param name="searchKey">The keyword to search employee.</param>
        /// <returns>IEnumerable employee.</returns>
        [HttpGet]
        public IEnumerable<dynamic> RenderEmployeeSelectList(int? roleId = null, string searchKey = "")
        {
            var getEmployeeByRoleCriteria = new GetEmployeeByRoleCriteria() { RoleId = roleId };
            IEnumerable<EmployeeModel> employees = _employeeService.GetEmployeeByRole(getEmployeeByRoleCriteria);
            IEnumerable<dynamic> selectListEmployee = null;

            if (!string.IsNullOrEmpty(searchKey))
            {
                employees = GetEmployeeBySearchKey(employees, searchKey);
            }

            if (employees != null)
            {
                selectListEmployee = employees.Select(x => new { x.FullName, x.NickName, x.EmployeeId, x.ProjectRoleId });
            }

            return selectListEmployee;
        }

        /// <summary>
        /// Render Project Status Select List.
        /// </summary>
        /// <returns>Array of Project Status.</returns>
        [HttpGet]
        public IEnumerable<SelectListModel> RenderProjectStatusSelectList()
        {
            IEnumerable<SelectListModel> data = EnumStatus.GetAllStatusOfProject();
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

        private IEnumerable<string> RetrieveErrorMessages()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
            return errors;
        }

        /// <summary>
        /// Get Period of time.
        /// </summary>
        /// <param name="period">Period from filter.</param>
        /// <returns>Date time.</returns>
        private DateTime GetPeriodOfTime(int period)
        {
            DateTime startDate = new DateTime();
            DateTime now = DateTime.Now;
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

        /// <summary>
        /// Get employee by search keyword.
        /// </summary>
        /// <param name="employees">The list employee.</param>
        /// <param name="searchKey">The keyword to get employee.</param>
        /// <returns>The IEnumerable Employee Info.</returns>
        private IEnumerable<EmployeeModel> GetEmployeeBySearchKey(IEnumerable<EmployeeModel> employees, string searchKey)
        {
            List<EmployeeModel> searchEmployees = new List<EmployeeModel>();

            foreach (EmployeeModel employee in employees)
            {
                if (CheckMatchSearchKey(employee.NickName, searchKey) || CheckMatchSearchKey(employee.FullName, searchKey))
                {
                    searchEmployees.Add(employee);
                }
            }

            return searchEmployees;
        }

        /// <summary>
        /// Check name of employee matches with search key.
        /// </summary>
        /// <param name="name">The full name or nick name of employee.</param>
        /// <param name="searchKey">The search keyword.</param>
        /// <returns>True or false.</returns>
        private bool CheckMatchSearchKey(string name, string searchKey)
        {
            string pattern = @"\A" + searchKey;
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(name);

            if (match.Success)
            {
                return true;
            }

            return false;
        }
    }
}
