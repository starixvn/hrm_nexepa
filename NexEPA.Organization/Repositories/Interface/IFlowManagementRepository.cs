// --------------------------------------------------------------------------
// <copyright file="IFlowManagementRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.19">Created</change>
// </history>

namespace NexEPA.Organization.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Organization.Models;

    /// <summary>
    /// Interface Flow Management Repository.
    /// </summary>
    public interface IFlowManagementRepository
    {
        IEnumerable<EPAFlowModel> GetFlowManagement(ICriteria criteria);
    }
}
