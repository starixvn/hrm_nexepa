// --------------------------------------------------------------------------
// <copyright file="AddKPIGroupWeightCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add KPI Group Weight Criteria.
    /// </summary>
    public class AddKPIGroupWeightCriteria : CriteriaBase
    {
        public int TargetTemplateId { get; set; }

        public string KPIGroups { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_SetKPIGroup";
        }
    }
}
