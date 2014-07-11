// --------------------------------------------------------------------------
// <copyright file="DepartmentService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Organization.Criteria;
    using NexEPA.Organization.Models;
    using NexEPA.Organization.Repositories.Implement;
    using NexEPA.Organization.Repositories.Interface;
    using NexEPA.Organization.Services.Interface;

    /// <summary>
    /// Department Service.
    /// </summary>
    public class DepartmentService : BaseService, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository = null)
        {
            this._departmentRepository = departmentRepository ?? new DepartmentRepository();
        }

        public List<DepartmentModel> GetAllDepartment(GetAllDepartmentCriteria criteria)
        {
            List<DepartmentModel> department = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                department = _departmentRepository.GetAllDepartment(criteria);
            }

            return department;
        }

        public CommandResult AddDepartment(AddDepartmentCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult UpdateDepartment(UpdateDepartmentCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Update(criteria);
            }

            return result;
        }

        public CommandResult DeleteDepartment(DeleteDepartmentCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }
    }
}
