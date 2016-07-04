// --------------------------------------------------------------------------
// <copyright file="IDepartmentService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Organization.Criteria;
    using NexEPA.Organization.Models;

    /// <summary>
    /// Interface Department Service.
    /// </summary>
    public interface IDepartmentService
    {
        List<DepartmentModel> GetAllDepartment(GetAllDepartmentCriteria criteria);

        CommandResult AddDepartment(AddDepartmentCriteria criteria);

        CommandResult UpdateDepartment(UpdateDepartmentCriteria criteria);

        CommandResult DeleteDepartment(DeleteDepartmentCriteria criteria);
    }
}
