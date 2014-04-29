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
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.ViewModels;

    /// <summary>
    /// The interface of Employee services.
    /// </summary>
    public interface IEmployeeService
    {
        List<EmployeeItem> GetAllEmployee(GetAllEmployeeCriteria criteria);

        List<Group> GetAllEmployeeGroup(GetAllEmployeeGroupCriteria criteria);

        List<EmployeeItem> GetEmployeeByProjectId(GetEmployeeByProjectIdCriteria criteria);
    }
}
