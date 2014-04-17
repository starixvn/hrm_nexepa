// -----------------------------------------------------------------------
// <copyright file="EmployeeServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Services.Implement
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Services;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Repositories.Implement;
    using NexEPA.Employee.Repositories.Interface;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Employee.ViewModels;

    /// <summary>
    /// The implement of Employee services.
    /// </summary>
    public class EmployeeServices : BaseService, IEmployeeServices
    {
        private IEmployeeRepositories _employeeRepositories;

        public EmployeeServices()
        {
            _employeeRepositories = new EmployeeRepositories();
        }

        /// <summary>
        /// Get all employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list Employee Item.</returns>
        public List<EmployeeItem> GetAllEmployee(GetAllEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employees = new List<EmployeeItem>();

            if (criteria.IsValid())
            {
                IEnumerable<EmployeeInfo> rawEmployees = _employeeRepositories.GetAllEmployee(criteria);

                foreach (var employee in rawEmployees)
                {
                    string joinedDate = employee.JoinedDate != null
                        ? ((DateTime)employee.JoinedDate).ToString(Constants.ShortDateStringFormat)
                        : string.Empty;

                    var employeeItem = new EmployeeItem
                    {
                        EmployeeId = employee.EmployeeId,
                        Code = employee.Code,
                        FullName = employee.FullName,
                        NickName = employee.NickName,
                        Email = employee.Email,
                        Skype = employee.Skype,
                        JoinedDate = joinedDate,
                        Group = employee.Group,
                        JobTitle = employee.JobTitle
                    };

                    employees.Add(employeeItem);
                }
            }

            return employees;
        }

        /// <summary>
        /// Get all employee's group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list Group.</returns>
        public List<Group> GetAllEmployeeGroup(GetAllEmployeeGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeesGroup = new List<Group>();

            if (criteria.IsValid())
            {
                IEnumerable<GroupInfo> rawGroups = GetData<GroupInfo>(criteria);

                foreach (var group in rawGroups)
                {
                    var groupItem = new Group
                    {
                        GroupId = group.GroupId,
                        Name = group.Name
                    };

                    employeesGroup.Add(groupItem);
                }
            }

            return employeesGroup;
        }
    }
}
