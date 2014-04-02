// -----------------------------------------------------------------------
// <copyright file="UpdateKPIGroupCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPIGroup
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update KPI group criteria.
    /// </summary>
    public class UpdateKPIGroupCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public bool IsDisabled { get; set; }

        public short TargetTypeId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_Update";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
