// -----------------------------------------------------------------------
// <copyright file="KPIGroupItem.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.17">Created</change>
// </history>

namespace NexEPA.KPI.ViewModel
{
    using System.Collections.Generic;
    using NexEPA.KPI.ViewModel.KPI;

    /// <summary>
    /// The view model of KPI Group.
    /// </summary>
    public class KPIGroupItem
    {
        public KPIGroupItem()
        {
            Name = string.Empty;
            Description = string.Empty;
            Form = string.Empty;
            ChildKPIs = new List<KPIItem>();
        }

        public int KPIGroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public int TargetTypeId { get; set; }

        public string Form { get; set; }

        public List<KPIItem> ChildKPIs { get; set; }
    }
}
