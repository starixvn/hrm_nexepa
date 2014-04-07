// --------------------------------------------------------------------------
// <copyright file="AddKPIToTargetTemplateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.7">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Add KPI To Target Template Criteria.
    /// </summary>
    public class AddKPIToTargetTemplateCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public string KPIIds { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_AddKPI";
        }
    }
}
