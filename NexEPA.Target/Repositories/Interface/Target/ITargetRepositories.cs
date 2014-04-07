// -----------------------------------------------------------------------
// <copyright file="ITargetRepositories.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Interface.Target
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Target.Models.Target;

    /// <summary>
    /// The interface of Target Repository.
    /// </summary>
    public interface ITargetRepositories
    {
        IList<TargetInfo> GetTarget(ICriteria criteria);

        TargetInfo GetTargetById(ICriteria criteria);
    }
}
