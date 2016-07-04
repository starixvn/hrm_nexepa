// -----------------------------------------------------------------------
// <copyright file="KPIGroupViewModel.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.17">Created</change>
// </history>

namespace NexEPA.KPI.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.KPI.ViewModels;

    /// <summary>
    /// The view model of KPI Group.
    /// </summary>
    public class KPIGroupViewModel
    {
        public int KPIGroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Weight { get; set; }

        public int TargetTypeId { get; set; }

        public string Form { get; set; }

        public bool IsDisabled { get; set; }

        public int NumberKPI { get; set; }

        public bool Selected { get; set; }
    }
}
