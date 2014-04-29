// -----------------------------------------------------------------------
// <copyright file="UpdateKPIGroupStatusCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.29">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPIGroup
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update status of KPI Group criteria.
    /// </summary>
    public class UpdateKPIGroupStatusCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_UpdateStatus";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
