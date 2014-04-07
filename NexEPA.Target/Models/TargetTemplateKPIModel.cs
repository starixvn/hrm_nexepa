// --------------------------------------------------------------------------
// <copyright file="TargetTemplateKPIModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.7">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Target Template KPI Model.
    /// </summary>
    public class TargetTemplateKPIModel
    {
        public int TargetId { get; set; }

        public string KPIIds { get; set; }
    }
}
