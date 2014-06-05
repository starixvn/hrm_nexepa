// --------------------------------------------------------------------------
// <copyright file="IDepartmentService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Organization.Criteria;
    using NexEPA.Organization.Models;

    /// <summary>
    /// Interface Department Service.
    /// </summary>
    public interface IDepartmentService
    {
        DepartmentModel GetAllDepartment(GetAllDepartmentCriteria criteria);
    }
}
