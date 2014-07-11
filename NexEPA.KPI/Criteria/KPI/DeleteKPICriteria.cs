// -----------------------------------------------------------------------
// <copyright file="DeleteKPICriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.31">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete KPI criteria.
    /// </summary>
    public class DeleteKPICriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_Delete";
        }

        public override bool IsValid()
        {
            return KPIId > 0;
        }
    }
}
