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
    using System.Web.Mvc;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services;
    using System.Linq;
    using NexEPA.Core.Models;
    using NexEPA.Core.Enums;
    using System.Collections.Generic;

    /// <summary>
    /// Project Controller.
    /// </summary>
    public class ProjectController : JsonController
    {
        private readonly GroupService _groupService;

        private readonly EmployeeService _employeeService;

        public ProjectController()
            : this(null, null)
        {
        }

        public ProjectController(GroupService groupService, EmployeeService employeeService)
        {
            this._groupService = groupService ?? new GroupService();
            this._employeeService = employeeService ?? new EmployeeService();
        }

        public ActionResult RenderPMSelectList()
        {
            var criteria = new GetEmployeeByRoleCriteria() { RoleId = null };
            var employees = _employeeService.GetData<Employee>(criteria);
            var selectListEmployee = employees.Select(x => new { x.FullName, x.EmployeeId });

            return Json(selectListEmployee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RenderProjectStatusSelectList()
        {
            var statusList = new List<SelectListItem>
                    {
                        new SelectListItem() { Text = ProjectStatus.Active.ToString(), Value = ((short)ProjectStatus.Active).ToString() },
                        new SelectListItem() { Text = ProjectStatus.Closed.ToString(), Value = ((short)ProjectStatus.Closed).ToString() }
                    };

            return Json(statusList, JsonRequestBehavior.AllowGet);
        }
    }
}
