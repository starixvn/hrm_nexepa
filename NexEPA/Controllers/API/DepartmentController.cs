// --------------------------------------------------------------------------
// <copyright file="DepartmentController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using NexEPA.Organization.Criteria;
    using NexEPA.Organization.Models;
    using NexEPA.Organization.Services.Implement;
    using NexEPA.Organization.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Department Controller.
    /// </summary>
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController()
            : this(null)
        {
        }

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService ?? new DepartmentService();
        }

        [ApiAuthorize(Roles = "SA")]
        public DepartmentModel GetAllDeparment()
        {
            var getAllDepartmentCriteria = new GetAllDepartmentCriteria();
            DepartmentModel department = _departmentService.GetAllDepartment(getAllDepartmentCriteria);

            return department;
        }
    }
}
