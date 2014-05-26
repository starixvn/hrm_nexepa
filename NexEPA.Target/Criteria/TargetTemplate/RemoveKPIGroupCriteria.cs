// --------------------------------------------------------------------------
// <copyright file="RemoveKPIGroupCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.26">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RemoveKPIGroupCriteria : CriteriaBase
    {
        public int TargetTemplateId { get; set; }

        public int KPIGroupId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return TargetTemplateId > 0 && KPIGroupId > 0;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_RemoveKPIGroup";
        }
    }
}
