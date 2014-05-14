// -----------------------------------------------------------------------
// <copyright file="GetKPIByIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.31">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get KPI by ID criteria.
    /// </summary>
    public class GetKPIByIdCriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_GetById";
        }

        public override bool IsValid()
        {
            return KPIId > 0;
        }
    }
}
