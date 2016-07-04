// --------------------------------------------------------------------------
// <copyright file="DepartmentRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Employee.Models;
    using NexEPA.Organization.Models;
    using NexEPA.Organization.Repositories.Interface;

    /// <summary>
    /// Department Repository.
    /// </summary>
    public class DepartmentRepository : IDepartmentRepository
    {
        /// <summary>
        /// Get All Department.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>IEnumerable DepartmentModel.</returns>
        public List<DepartmentModel> GetAllDepartment(ICriteria criteria)
        {
            List<DepartmentModel> departments = null;
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                departments = db.Query<DepartmentModel, EmployeeModel, EmployeeModel, EmployeeModel, DepartmentModel>(
                   (d, e1, e2, e3) =>
                   {
                       d.Creator = e1;
                       d.Manager = e2;
                       d.Modifier = e3;
                       return d;
                   },
                   "CreatedBy, ManagerId, ModifiedBy",
                   criteria.GetSpParams()).ToList();
            }

            if (departments.Count > 0)
            {
                departments.ForEach(x => x.Children.AddRange(departments.Where(y => y.ParentId == x.DepartmentId)));
            }

            return departments;
        }
    }
}
