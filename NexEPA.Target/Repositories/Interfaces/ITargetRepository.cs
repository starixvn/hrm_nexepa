// -----------------------------------------------------------------------
// <copyright file="ITargetRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Target.Models;

    /// <summary>
    /// The interface of Target Repository.
    /// </summary>
    public interface ITargetRepository
    {
        IEnumerable<TargetModel> GetTarget(ICriteria criteria);

        TargetModel GetTargetById(ICriteria criteria);

        IEnumerable<KPIModel> GetKPIByTargetId(ICriteria criteria);

        IEnumerable<EmployeeModel> GetTargetEmployeeByTargetId(ICriteria criteria);
    }
}
