// -----------------------------------------------------------------------
// <copyright file="EmployeeController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Services.Implement;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Employee.ViewModels;

    /// <summary>
    /// Employee controller.
    /// </summary>
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeServices;

        public EmployeeController()
            : this(null)
        {
        }

        public EmployeeController(EmployeeService employeeServices)
        {
            _employeeServices = employeeServices ?? new EmployeeService();
        }

        [HttpGet]
        public List<EmployeeItem> GetAllEmployee(
            string keyword = "", 
            int? groupId = null, 
            bool? isDisabled = null)
        {
            var criteria = new GetAllEmployeeCriteria
            {
                IsDisabled = isDisabled,
                Keyword = keyword,
                GroupId = groupId
            };

            return _employeeServices.GetAllEmployee(criteria);
        }

        [HttpGet]
        public List<Group> GetAllGroup()
        {
            var criteria = new GetAllEmployeeGroupCriteria();
            return _employeeServices.GetAllEmployeeGroup(criteria);
        }

        [HttpGet]
        public List<EmployeeItem> GetEmployeeByProjectId(
            int? projectId = null, 
            string keyword = "")
        {
            var criteria = new GetEmployeeByProjectIdCriteria
            {
                ProjectId = projectId,
                Keyword = keyword
            };

            return _employeeServices.GetEmployeeByProjectId(criteria);
        }
    }
}