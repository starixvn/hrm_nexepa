// --------------------------------------------------------------------------
// <copyright file="TargetKPIGroupWeight.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using NexEPA.Target.Models.Target;

    /// <summary>
    /// Target KPI Group Weight.
    /// </summary>
    public class TargetKPIGroupWeight
    {
        public int TargetTemplateId { get; set; }

        public List<TargetKPIGroup> KPIGroups { get; set; }
    }
}
