// -----------------------------------------------------------------------
// <copyright file="EmployeeService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Repositories.Implement;
    using NexEPA.Employee.Repositories.Interface;
    using NexEPA.Employee.Services.Interface;

    /// <summary>
    /// The implement of Employee service.
    /// </summary>
    public class EmployeeService : BaseService, IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository = null)
        {
            _employeeRepository = employeeRepository ?? new EmployeeRepository();
        }

        /// <summary>
        /// Get all employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetAllEmployee(GetAllEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employees = null;

            if (criteria.IsValid())
            {
                employees = _employeeRepository.GetAllEmployee(criteria);
            }

            return employees;
        }

        /// <summary>
        /// Get all employee's group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Group.</returns>
        public IEnumerable<Group> GetAllEmployeeGroup(GetAllEmployeeGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<Group> groups = null;

            if (criteria.IsValid())
            {
                groups = GetData<Group>(criteria);
            }

            return groups;
        }

        /// <summary>
        /// Get employee by project ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByProjectId(GetEmployeeByProjectIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employees = null;

            if (criteria.IsValid())
            {
                employees = _employeeRepository.GetEmployeeByProjectId(criteria);
            }

            return employees;
        }

        /// <summary>
        /// Get employees by role.
        /// </summary>
        /// <param name="criteria">Get Employee By Role Criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByRole(GetEmployeeByRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employees = null;

            if (criteria.IsValid())
            {
                employees = _employeeRepository.GetAllEmployee(criteria);
            }

            return employees;
        }

        /// <summary>
        /// Get employee by current user id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(GetAllEmployeeByCurrentUserIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employees = null;

            if (criteria.IsValid())
            {
                employees = _employeeRepository.GetEmployeeByCurrentUserId(criteria);
            }

            return employees;
        }
    }
}
