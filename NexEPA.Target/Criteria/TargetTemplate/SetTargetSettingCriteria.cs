// --------------------------------------------------------------------------
// <copyright file="SetTargetSettingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.9">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Set Target Setting Criteria.
    /// </summary>
    public class SetTargetSettingCriteria : CriteriaBase
    {
        public int TargetTypeId { get; set; }

        public int TargetTemplateId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return TargetTypeId > 0 && TargetTemplateId > 0;
        }

        public override string GetSettingKey()
        {
            return "TargetSetting_Set";
        }
    }
}
