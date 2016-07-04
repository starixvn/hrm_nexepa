// --------------------------------------------------------------------------
// <copyright file="GetSelectedKPIGroupCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using Fanex.Data.Repository;

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
