// --------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.19">Created</change>
// </history>

namespace NexEPA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using NexEPA.Core.Enums;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;

    /// <summary>
    /// Project Controller.
    /// </summary>
    public class ProjectController : JsonController
    {
        private readonly ProjectService _projectService;

        private readonly EmployeeService _employeeService;

        public ProjectController()
            : this(null, null)
        {
        }

        public ProjectController(ProjectService projectService, EmployeeService employeeService)
        {
            this._projectService = projectService ?? new ProjectService();
            this._employeeService = employeeService ?? new EmployeeService();
        }

        public ActionResult RenderPMSelectList()
        {
            GetEmployeeByRoleCriteria criteria = new GetEmployeeByRoleCriteria() { RoleId = null };
            IEnumerable<Employee> employees = _employeeService.GetData<Employee>(criteria);
            var selectListEmployee = employees.Select(x => new { x.FullName, x.EmployeeId });

            return Json(selectListEmployee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderProjectStatusSelectList()
        {
            Array data = new[]
                {
                    new SelectListModel { Id = (int)ProjectStatus.Active, Name = ProjectStatus.Active.ToString() },
                    new SelectListModel { Id = (int)ProjectStatus.Closed, Name = ProjectStatus.Closed.ToString() }
                };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Project period filter select list.
        /// </summary>
        /// <returns>Select list data.</returns>
        public ActionResult RenderProjectPeriodSelectList()
        {
            Array statusList = new[]
                    {
                        new SelectListModel() { Name = "This month", Id = 1 },
                        new SelectListModel() { Name = "Last 3 months", Id = 2 },
                        new SelectListModel() { Name = "Last 6 months", Id = 3 },
                        new SelectListModel() { Name = "Last 12 months", Id = 4 }
                    };

            return Json(statusList, JsonRequestBehavior.AllowGet);
        }
    }
}
