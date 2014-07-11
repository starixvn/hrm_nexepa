// -----------------------------------------------------------------------
// <copyright file="IEmployeeRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Employee.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Employee.Models;

    /// <summary>
    /// The interface of Employee Repository.
    /// </summary>
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetAllEmployee(ICriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByProjectId(ICriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(ICriteria criteria);
    }
}
