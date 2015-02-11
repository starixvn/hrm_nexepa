// -----------------------------------------------------------------------
// <copyright file="AuthorityController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Authority.Criteria;
    using NexEPA.Authority.Models;
    using NexEPA.Authority.Services.Implement;
    using NexEPA.Authority.Services.Interface;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services.Implement;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// The Authority Controller.
    /// </summary>
    public class AuthorityController : BaseController
    {
        private IAuthorityService _authorityService;

        private ICommonService _commonService;

        private IEmployeeService _employeeService;

        public AuthorityController()
            : this(null, null, null)
        {
        }

        public AuthorityController(
            IAuthorityService authorityService,
            ICommonService commonService,
            IEmployeeService employeeService)
        {
            _authorityService = authorityService ?? new AuthorityService();
            _commonService = commonService ?? new CommonService();
            _employeeService = employeeService ?? new EmployeeService();
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageRole, Policy = Policy.Read)]
        public IEnumerable<FunctionModel> GetFunctionByRole(int roleId)
        {
            var criteria = new GetFunctionByRoleCriteria()
            {
                RoleId = roleId
            };

            return _authorityService.GetFunctionByRole(criteria);
        }

        [HttpGet]
        public IEnumerable<RoleSystemModel> GetAllSystemRole()
        {
            var criteria = new GetAllSystemRoleCriteria();

            return _authorityService.GetAllSystemRole(criteria);
        }

        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageRole, Policy = Policy.Host)]
        public CommandResult AssignPermissionToRole(AssignPermissionToRoleCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _authorityService.AssignPermissionToRole(criteria);
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetManageRolePermission()
        {
            int policy = _commonService.GetPermission(EPAFunctionKeys.ManageRole);
            return policy;
        }

        /// <summary>
        /// Function to get all employees.
        /// </summary>
        /// <param name="keyword">The search keyword.</param>
        /// <param name="groupId">The Group Id.</param>
        /// <param name="isDisabled">Is Disabled.</param>
        /// <returns>The enumerator Employee Model.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageUser, Policy = Policy.Read)]
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

        /// <summary>
        /// Get roles by employee ID.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>The enumerator Role System Model.</returns>
        public IEnumerable<RoleSystemModel> GetRolesByEmployeeId(int employeeId)
        {
            var criteria = new GetRoleByEmployeeIdCriteria
            {
                EmployeeId = employeeId
            };

            return _authorityService.GetRolesByEmployeeId(criteria);
        }

        /// <summary>
        /// Update role.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageUser, Policy = Policy.Host)]
        public CommandResult UpdateRole(UpdateRoleOfEmployeeCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _authorityService.UpdateRole(criteria);
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetManageUserPermission()
        {
            int policy = _commonService.GetPermission(EPAFunctionKeys.ManageUser);
            return policy;
        }

        /// <summary>
        /// Determines whether [is only member].
        /// </summary>
        /// <returns>The object data.</returns>
        [HttpGet]
        public object IsOnlyMember()
        {
            var criteria = new EmployeeIsOnlyMemberCriteria
            {
                EmployeeId = CurrentUser.UserId
            };

            bool isOnlyMember = _authorityService.IsOnlyMember(criteria);
            var resultData = new { IsOnlyMember = isOnlyMember, EmployeeId = CurrentUser.UserId };

            return resultData;
        }

        /// <summary>
        /// Checks the right to view person report of employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>The object data.</returns>
        [HttpGet]
        public object CheckRightToViewPersonReportOfEmployee(int employeeId)
        {
            var criteria = new GetAllEmployeeByCurrentUserIdCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                Keyword = string.Empty
            };

            bool hasViewRight = _authorityService.CheckRightToViewPersonReportOfEmployee(employeeId, criteria);

            return new { HasViewRight = hasViewRight };
        }
    }
}