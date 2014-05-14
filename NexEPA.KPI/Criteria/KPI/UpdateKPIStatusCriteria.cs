// -----------------------------------------------------------------------
// <copyright file="UpdateKPIStatusCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.29">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update status of KPI criteria.
    /// </summary>
    public class UpdateKPIStatusCriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_UpdateStatus";
        }

        public override bool IsValid()
        {
            return KPIId > 0;
        }
    }
}
