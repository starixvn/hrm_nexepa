// -----------------------------------------------------------------------
// <copyright file="GetKPIByTargetIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.17">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.TargetKPI
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get KPIs by Target ID criteria.
    /// </summary>
    public class GetKPIByTargetIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_GetByTarget";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
