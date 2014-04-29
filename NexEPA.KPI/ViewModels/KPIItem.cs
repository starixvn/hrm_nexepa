// -----------------------------------------------------------------------
// <copyright file="KPIItem.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.ViewModels
{
    using System.Collections.Generic;

    /// <summary>
    /// The view model of KPI.
    /// </summary>
    public class KPIItem
    {
        public KPIItem() 
        {
            Name = string.Empty;
            Description = string.Empty;
            KPIGroupIds = new List<int>();
            KPIGroupNames = new List<string>();
            TargetTypeName = string.Empty;
        }

        public int KPIId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsRequireComment { get; set; }

        public bool IsDisabled { get; set; }

        public List<int> KPIGroupIds { get; set; }

        public string KPIGroupName { get; set; }

        public List<string> KPIGroupNames { get; set; }

        public int TargetTypeId { get; set; }

        public int KPIGroupId { get; set; }

        public string TargetTypeName { get; set; }

        public int Weight { get; set; }

        public bool? Selected { get; set; }
    }
}
