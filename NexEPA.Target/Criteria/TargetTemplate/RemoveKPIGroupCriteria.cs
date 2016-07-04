// --------------------------------------------------------------------------
// <copyright file="RemoveKPIGroupCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.26">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Remove KPI Group Criteria.
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
