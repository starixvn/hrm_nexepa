// --------------------------------------------------------------------------
// <copyright file="TargetTemplateRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.9">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Target.Models;
    using NexEPA.Target.Repositories.Interface;

    /// <summary>
    /// Target Template Repository.
    /// </summary>
    public class TargetTemplateRepository : ITargetTemplateRepository
    {
        /// <summary>
        /// Get Target Setting.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>TargetSetting Models.</returns>
        public IEnumerable<TargetSettingModel> GetTargetSetting(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var entity = db.Query<TargetSettingModel, TargetTemplateModel, TargetSettingModel>(
                    (ts, tg) =>
                    {
                        ts.TargetTemplate = tg;
                        return ts;
                    },
                    "TargetTemplateId",
                    criteria.GetSpParams());

                return entity;
            }
        }
    }
}
