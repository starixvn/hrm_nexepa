// -----------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Employee.Repositories.Implements
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Repositories.Interfaces;

    /// <summary>
    /// The implement of Employee Repositories.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Get all employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List Employee Info.</returns>
        public IEnumerable<Employee> GetAllEmployee(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<Employee, Group, JobTitle, Employee>(
                    (emp, gr, jb) =>
                    {
                        emp.Group = gr;
                        emp.JobTitle = jb;
                        return emp;
                    },
                    "GroupId,JobTitleId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get employee by project ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The IEnumerable Employee Info.</returns>
        public IEnumerable<Employee> GetEmployeeByProjectId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<Employee, Group, Employee>(
                    (empIn, grIn) =>
                    {
                        empIn.Group = grIn;
                        return empIn;
                    },
                    "GroupId",
                    criteria.GetSpParams());
            }
        }
    }
}
