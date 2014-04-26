// --------------------------------------------------------------------------
// <copyright file="GetAllKPIByKPIGroupCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get All KPI By KPIGroup Criteria.
    /// </summary>
    public class GetAllKPIByKPIGroupCriteria : CriteriaBase
    {
        public int TargetTemplateId { get; set; }

        public override bool IsValid()
        {
            return TargetTemplateId > 0;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_GetAllKPIByKPIGroups";
        }
    }
}
