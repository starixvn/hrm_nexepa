// --------------------------------------------------------------------------
// <copyright file="GetTargetSettingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.9">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Default Target Template Criteria.
    /// </summary>
    public class GetTargetSettingCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "TargetSetting_Get";
        }
    }
}
