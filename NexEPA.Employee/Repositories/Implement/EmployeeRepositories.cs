// -----------------------------------------------------------------------
// <copyright file="EmployeeRepositories.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Employee.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Repositories.Interface;
    using NexEPA.Employee.ViewModels;

    /// <summary>
    /// The implement of Employee Repositories.
    /// </summary>
    public class EmployeeRepositories : IEmployeeRepositories
    {
        /// <summary>
        /// Get all employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List Employee Info.</returns>
        public IEnumerable<EmployeeInfo> GetAllEmployee(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeInfo, Group, JobTitle, EmployeeInfo>(
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
        public IEnumerable<EmployeeInfo> GetEmployeeByProjectId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeInfo, Group, EmployeeInfo>(
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
