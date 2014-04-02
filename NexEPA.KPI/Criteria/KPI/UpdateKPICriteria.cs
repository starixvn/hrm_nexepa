// -----------------------------------------------------------------------
// <copyright file="UpdateKPICriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.31">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update KPI criteria.
    /// </summary>
    public class UpdateKPICriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int KPIGroupId { get; set; }

        public bool IsRequireComment { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_Update";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
