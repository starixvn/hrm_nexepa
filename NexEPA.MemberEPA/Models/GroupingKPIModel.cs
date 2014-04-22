// --------------------------------------------------------------------------
// <copyright file="GroupingKPIModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model.KPI;

    /// <summary>
    /// Grouping KPI Model.
    /// </summary>
    public class GroupingKPIModel
    {
        public string KPIGroupName { get; set; }

        public List<KPIInfo> KPIGroupItems { get; set; }
    }
}
