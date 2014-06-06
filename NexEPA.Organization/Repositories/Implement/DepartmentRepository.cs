// --------------------------------------------------------------------------
// <copyright file="DepartmentRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Repositories.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
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
        public Tuple<DepartmentModel, List<DepartmentModel>> GetAllDepartment(ICriteria criteria)
        {
            List<DepartmentModel> departments = null;
            DepartmentModel department = null;
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
                List<DepartmentModel> groupingItem = (from item in departments
                                                      group item by item.ParentId into item
                                                      select new DepartmentModel
                                                      {
                                                          ParentId = item.Key,
                                                          Children = item.ToList()
                                                      }).Where(x => x.ParentId != null).ToList();

                departments.ToList().ForEach(x => x.Children.AddRange(groupingItem.Where(y => y.ParentId == x.DepartmentId)));
                department = departments.Where(x => x.ParentId == null).SingleOrDefault();
            }

            return Tuple.Create(department, departments);
        }
    }
}
