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
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services.Implement;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Employee controller.
    /// </summary>
    public class EmployeeController : BaseController
    {
        private const string ExcludedEmployeeGroupIdSetting = "ExcludedEmployeeGroupId";
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
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
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
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public IList<SelectListModel> GetAllGroup()
        {
            var criteria = new GetAllEmployeeGroupCriteria
            {
                ExcludedGroupId = ConfigurationManager.AppSettings[ExcludedEmployeeGroupIdSetting]
            };
            IEnumerable<Group> groups = _employeeService.GetAllEmployeeGroup(criteria);
            IList<SelectListModel> selectListGroups = groups.Select(t => new SelectListModel { Id = t.GroupId, Name = t.Name }).ToList();

            return selectListGroups;
        }

        /// <summary>
        /// Retrieves all group by actor user.
        /// </summary>
        /// <returns>All group by actor user.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public IList<SelectListModel> RetrieveAllGroupByActorUser()
        {
            var criteria = new GetAllEmployeeGroupByActorUserCriteria
            {
                ActorUserId = CurrentUser.UserId,
                ExcludedGroupId = ConfigurationManager.AppSettings[ExcludedEmployeeGroupIdSetting]
            };
            IEnumerable<Group> groups = _employeeService.GetAllEmployeeGroupByActorUser(criteria);
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
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
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

        /// <summary>
        /// Gets the employee by current user identifier.
        /// </summary>
        /// <param name="keyword">The keyword.</param>
        /// <returns>The employee by current user identifier.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(string keyword = "")
        {
            var criteria = new GetAllEmployeeByCurrentUserIdCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                Keyword = keyword,
                ExcludedGroupId = ConfigurationManager.AppSettings[ExcludedEmployeeGroupIdSetting]
            };

            IEnumerable<EmployeeModel> employees = _employeeService.GetEmployeeByCurrentUserId(criteria);
            return employees;
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public EmployeeModel GetEmployeeById(int employeeId)
        {
            var criteria = new GetEmployeeByIdCriteria
            {
                EmployeeId = employeeId
            };
            EmployeeModel employee = _employeeService.GetEmployeeById(criteria);

            return employee;
        }

        /// <summary>
        /// Get Employee In A Department.
        /// </summary>
        /// <param name="departmentId">Department Id.</param>
        /// <returns>List Employee Model.</returns>
        public EmployeeDepartmentModel GetEmployeeInDepartment(int departmentId)
        {
            IEnumerable<EmployeeModel> employees = null;
            var employeeDepartment = new EmployeeDepartmentModel();

            if (departmentId > 0)
            {
                var getEmployeeByDepartment = new GetEmployeeByDepartmentCriteria() { DepartmentId = departmentId };
                employees = _employeeService.GetByDepartment(getEmployeeByDepartment);
            }

            if (departmentId < 0)
            {
                var getEmployeeByGroup = new GetEmployeeByGroupCriteria() { GroupId = Math.Abs(departmentId) };
                employees = _employeeService.GetByGroup(getEmployeeByGroup);
            }

            if (employees != null)
            {
                employeeDepartment.PersonalFlowSettingEmployees = employees.Where(x => x.IsExistsSetting == true);
                employeeDepartment.DefaultFlowSettingEmployees = employees.Where(x => x.IsExistsSetting == false);
            }

            return employeeDepartment;
        }

        /// <summary>
        /// Gets the employee by team project.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns>The employee by team project.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public IEnumerable<EmployeeModel> GetEmployeeByTeamProject(
            int? groupId,
            int? projectId)
        {
            var criteria = new GetEmployeeByTeamProjectCriteria
            {
                ActorUserId = CurrentUser.UserId,
                GroupId = groupId,
                ProjectId = projectId,
                ExcludedGroupId = ConfigurationManager.AppSettings[ExcludedEmployeeGroupIdSetting]
            };
            IEnumerable<EmployeeModel> employees = _employeeService.GetEmployeeByTeamProject(criteria);

            return employees;
        }

        /// <summary>
        /// Gets the redmine employees by team project.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="projectIds">The project ids.</param>
        /// <returns>The redmine employees by team project.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public IEnumerable<EmployeeModel> GetRedmineEmployeesByTeamProject(
            int? groupId,
            string projectIds)
        {
            var criteria = new GetRedmineEmployeesByTeamProjectCriteria
            {
                GroupId = groupId,
                ProjectIds = projectIds,
                ExcludedGroupId = ConfigurationManager.AppSettings[ExcludedEmployeeGroupIdSetting]
            };
            IEnumerable<EmployeeModel> employees = _employeeService.GetRedmineEmployeesByTeamProject(criteria);

            return employees;
        }

        /// <summary>
        /// Gets the redmine employees by team and group project.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="groupProjectId">The group project identifier.</param>
        /// <returns>The redmine employees by team and group project.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Employee, Policy = Policy.Read)]
        public IEnumerable<EmployeeModel> GetRedmineEmployeesByTeamGroupProject(
            int? groupId,
            int? groupProjectId)
        {
            var criteria = new GetRedmineEmployeesByTeamGroupProjectCriteria
            {
                GroupId = groupId,
                GroupProjectId = groupProjectId,
                ExcludedGroupId = ConfigurationManager.AppSettings[ExcludedEmployeeGroupIdSetting]
            };
            IEnumerable<EmployeeModel> employees = _employeeService.GetRedmineEmployeesByTeamGroupProject(criteria);

            return employees;
        }
    }
}