// --------------------------------------------------------------------------
// <copyright file="ITargetTemplateRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.9">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Target.Models;

    /// <summary>
    /// Interface Target Template Repository.
    /// </summary>
    public interface ITargetTemplateRepository
    {
        IEnumerable<TargetSettingModel> GetTargetSetting(ICriteria criteria);
    }
}
