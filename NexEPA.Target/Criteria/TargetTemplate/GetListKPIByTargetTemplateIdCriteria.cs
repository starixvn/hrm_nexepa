// --------------------------------------------------------------------------
// <copyright file="GetListKPIByTargetTemplateIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.8">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get List KPI By Target Template Id Criteria.
    /// </summary>
    public class GetListKPIByTargetTemplateIdCriteria : CriteriaBase
    {
        public int? TargetTemplateId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "KPI_GetByTargetTemplate";
        }
    }
}
