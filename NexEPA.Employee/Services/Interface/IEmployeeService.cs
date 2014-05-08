// -----------------------------------------------------------------------
// <copyright file="IEmployeeService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Criteria;

    /// <summary>
    /// The interface of Employee services.
    /// </summary>
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAllEmployee(GetAllEmployeeCriteria criteria);

        IEnumerable<Group> GetAllEmployeeGroup(GetAllEmployeeGroupCriteria criteria);

        IEnumerable<Employee> GetEmployeeByProjectId(GetEmployeeByProjectIdCriteria criteria);

        IEnumerable<Employee> GetEmployeeByRole(GetEmployeeByRoleCriteria criteria);
    }
}
