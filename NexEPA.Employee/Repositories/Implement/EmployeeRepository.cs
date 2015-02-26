// -----------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Employee.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Repositories.Interface;

    /// <summary>
    /// The implement of Employee Repositories.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Get all employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetAllEmployee(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeModel, Group, JobTitle, EmployeeModel>(
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
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByProjectId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeModel, Group, EmployeeModel>(
                    (emp, gr) =>
                    {
                        emp.Group = gr;
                        return emp;
                    },
                    "GroupId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get employees by current user id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeModel, Group, JobTitle, EmployeeModel>(
                    (emp, gr, job) =>
                    {
                        emp.Group = gr;
                        emp.JobTitle = job;
                        return emp;
                    },
                    "GroupId,JobTitleId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Gets the employee by team project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>EmployeeModel collection.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByTeamProject(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeModel>(criteria.GetSpParams());
            }
        }
    }
}
