// --------------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.11">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services;

    /// <summary>
    /// Employee API Controller.
    /// </summary>
    [ExceptionHandling]
    public class EmployeeController : ApiController
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

        public IEnumerable<Employee> GetEmployees()
        {
            GetEmployeeByIdCriteria criteria = new GetEmployeeByIdCriteria
            {
                EmployeeId = 1
            };

            IEnumerable<Employee> employees = _employeeService.GetData<Employee>(criteria);
            return employees;
        }
    }
}