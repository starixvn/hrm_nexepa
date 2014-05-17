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
    using System.Linq;
    using System.Web.Http;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services.Implement;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Employee controller.
    /// </summary>
    public class EmployeeController : BaseController
    {
        private IEmployeeService _employeeService;

        public EmployeeController()
            : this(null)
        {
        }

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? new EmployeeService();
        }

        /// <summary>
        /// Function to get all employees.
        /// </summary>
        /// <param name="keyword">The search keyword.</param>
        /// <param name="groupId">The Group Id.</param>
        /// <param name="isDisabled">Is Disabled.</param>
        /// <returns>The enumerator Employee Model.</returns>
        [HttpGet]
        public IEnumerable<EmployeeModel> GetAllEmployee(string keyword = "", int? groupId = null, bool? isDisabled = null)
        {
            var criteria = new GetAllEmployeeCriteria
            {
                IsDisabled = isDisabled,
                Keyword = keyword,
                GroupId = groupId
            };

            IEnumerable<EmployeeModel> employees = _employeeService.GetAllEmployee(criteria);
            return employees;
        }

        [HttpGet]
        public IList<SelectListModel> GetAllGroup()
        {
            var criteria = new GetAllEmployeeGroupCriteria();
            IEnumerable<Group> groups = _employeeService.GetAllEmployeeGroup(criteria);
            IList<SelectListModel> selectListGroups = groups.Select(t => new SelectListModel { Id = t.GroupId, Name = t.Name }).ToList();
            
            return selectListGroups;
        }

        /// <summary>
        /// Function to get employees by project.
        /// </summary>
        /// <param name="projectId">Project Id.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns>The enumerator Employee Model.</returns>
        [HttpGet]
        public IEnumerable<EmployeeModel> GetEmployeeByProjectId(int? projectId = null, string keyword = "")
        {
            var criteria = new GetEmployeeByProjectIdCriteria
            {
                ProjectId = projectId,
                Keyword = keyword
            };

            IEnumerable<EmployeeModel> employees = _employeeService.GetEmployeeByProjectId(criteria);

            return employees;
        }

        public IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(string keyword = "")
        {
            var criteria = new GetAllEmployeeByCurrentUserIdCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                Keyword = keyword
            };

            IEnumerable<EmployeeModel> employees = _employeeService.GetEmployeeByCurrentUserId(criteria);
            return employees;
        }
    }
}