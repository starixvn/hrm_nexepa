// -----------------------------------------------------------------------
// <copyright file="InsertKPICriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.28">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPI
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Insert new KPI criteria.
    /// </summary>
    public class InsertKPICriteria : CriteriaBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string KPIGroupIds { get; set; }

        public bool IsRequireComment { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_Insert";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(KPIGroupIds);
        }
    }
}
