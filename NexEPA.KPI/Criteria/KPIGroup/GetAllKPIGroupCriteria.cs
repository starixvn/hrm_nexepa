// -----------------------------------------------------------------------
// <copyright file="GetAllKPIGroupCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.17">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPIGroup
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get all KPI group criteria.
    /// </summary>
    public class GetAllKPIGroupCriteria : CriteriaBase
    {
        public bool? IsDisabled { get; set; }

        public int? TargetTypeId { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
