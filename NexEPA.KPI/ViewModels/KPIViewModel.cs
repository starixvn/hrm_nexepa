// -----------------------------------------------------------------------
// <copyright file="KPIViewModel.cs" company="Nexcel Solution Viet Nam">
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
    public class KPIViewModel
    {
        public KPIViewModel() 
        {
            KPIGroupIds = new List<int>();
            KPIGroupNames = new List<string>();
        }

        public int KPIId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasComment { get; set; }

        public bool IsRequireComment { get; set; }

        public bool IsDisabled { get; set; }

        public List<int> KPIGroupIds { get; set; }

        public string KPIGroupName { get; set; }

        public List<string> KPIGroupNames { get; set; }

        public int TargetTypeId { get; set; }

        public int KPIGroupId { get; set; }

        public int KPIGroupWeight { get; set; }

        public string TargetTypeName { get; set; }

        public int Weight { get; set; }

        public bool? Selected { get; set; }
    }
}
