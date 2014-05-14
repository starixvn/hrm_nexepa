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
    using NexEPA.Employee.Models;

    /// <summary>
    /// The interface of Employee service.
    /// </summary>
    public interface IEmployeeService
    {
        IEnumerable<EmployeeModel> GetAllEmployee(GetAllEmployeeCriteria criteria);

        IEnumerable<Group> GetAllEmployeeGroup(GetAllEmployeeGroupCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByProjectId(GetEmployeeByProjectIdCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByRole(GetEmployeeByRoleCriteria criteria);
    }
}
