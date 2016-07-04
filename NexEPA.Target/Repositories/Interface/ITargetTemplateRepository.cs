// --------------------------------------------------------------------------
// <copyright file="ITargetTemplateRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.9">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Target.Models;

    /// <summary>
    /// Interface Target Template Repository.
    /// </summary>
    public interface ITargetTemplateRepository
    {
        IEnumerable<TargetSettingModel> GetTargetSetting(ICriteria criteria);
    }
}
