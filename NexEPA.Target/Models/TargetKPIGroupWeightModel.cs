// --------------------------------------------------------------------------
// <copyright file="TargetKPIGroupWeightModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;

    /// <summary>
    /// Target KPI Group Weight.
    /// </summary>
    public class TargetKPIGroupWeightModel
    {
        public int TargetTemplateId { get; set; }

        public List<KPIGroupModel> KPIGroups { get; set; }
    }
}
