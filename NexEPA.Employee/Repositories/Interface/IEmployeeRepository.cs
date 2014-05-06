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
    using Fanex.NAP.Framework.Data;
    using NexEPA.Employee.Models;

    /// <summary>
    /// The interface of Employee Repositories.
    /// </summary>
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeInfo> GetAllEmployee(ICriteria criteria);

        IEnumerable<EmployeeInfo> GetEmployeeByProjectId(ICriteria criteria);
    }
}
