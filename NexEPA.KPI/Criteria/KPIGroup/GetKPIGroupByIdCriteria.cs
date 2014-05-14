// -----------------------------------------------------------------------
// <copyright file="GetKPIGroupByIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.27">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPIGroup
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get KPI group by id.
    /// </summary>
    public class GetKPIGroupByIdCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_GetById";
        }

        public override bool IsValid()
        {
            return KPIGroupId > 0;
        }
    }
}
