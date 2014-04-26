// --------------------------------------------------------------------------
// <copyright file="GetSelectedKPIGroupCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Selected KPI Group Criteria.
    /// </summary>
    public class GetSelectedKPIGroupCriteria : CriteriaBase
    {
        public int TargetTemplateId { get; set; }

        public override bool IsValid()
        {
            return TargetTemplateId > 0;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_GetKPIGroup";
        }
    }
}
