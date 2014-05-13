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
    using NexEPA.Target.Models.Target;

    /// <summary>
    /// The interface of Target Repository.
    /// </summary>
    public interface ITargetRepository
    {
        IEnumerable<TargetModel> GetTarget(ICriteria criteria);

        TargetModel GetTargetById(ICriteria criteria);

        IEnumerable<TargetKPIModel> GetKPIByTargetId(ICriteria criteria);
    }
}
