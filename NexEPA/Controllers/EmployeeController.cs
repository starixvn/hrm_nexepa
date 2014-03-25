// --------------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
// </history>

namespace NexEPA.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services;
    
    /// <summary>
    /// Employee Controller.
    /// </summary>
    public class EmployeeController : JsonController
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController()
            : this(null)
        { 
        }

        public EmployeeController(EmployeeService employeeService)
        {
            this._employeeService = employeeService ?? new EmployeeService();
        }

        public ActionResult Index()
        {
            GetEmployeeByIdCriteria criteria = new GetEmployeeByIdCriteria
            {
                EmployeeId = 1
            };

            IEnumerable<Employee> employees = _employeeService.GetData<Employee>(criteria);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(InsertEmployeeCriteria employee)
        {
            employee.EmployeeId = 7;
            employee.JoinedDate = DateTime.Now;
            CommandResult result = _employeeService.Insert(employee);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
