// -----------------------------------------------------------------------
// <copyright file="GetKPICriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPI criteria.
    /// </summary>
    public class GetKPICriteria : CriteriaBase
    {
        public int? TargetTypeId { get; set; }

        public int? KPIGroupId { get; set; }

        public bool? IsRequireComment { get; set; }

        public bool? IsDisabled { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_Get";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
