// -----------------------------------------------------------------------
// <copyright file="InsertKPIGroupCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Criteria.KPIGroup
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Insert new KPI group criteria.
    /// </summary>
    public class InsertKPIGroupCriteria : CriteriaBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public short TargetTypeId { get; set; }

        public bool IsDisabled { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_Insert";
        }

        public override bool IsValid()
        {
            return TargetTypeId >= 0 && !string.IsNullOrWhiteSpace(Name);
        }
    }
}