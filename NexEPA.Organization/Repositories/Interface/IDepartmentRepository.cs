// --------------------------------------------------------------------------
// <copyright file="IDepartmentRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Organization.Models;

    /// <summary>
    /// Interface Department Repository.
    /// </summary>
    public interface IDepartmentRepository
    {
        List<DepartmentModel> GetAllDepartment(ICriteria criteria);
    }
}
