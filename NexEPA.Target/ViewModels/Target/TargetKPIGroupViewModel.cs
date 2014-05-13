// -----------------------------------------------------------------------
// <copyright file="TargetKPIGroupViewModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.17">Created</change>
// </history>

namespace NexEPA.Target.ViewModels.Target
{
    using System.Collections.Generic;
    using NexEPA.Target.Models.Target;

    /// <summary>
    /// The Target KPI Group Item view model.
    /// </summary>
    public class TargetKPIGroupViewModel
    {
        public TargetKPIGroupViewModel()
        {
            KPIs = new List<TargetKPI>();
            Group = new TargetKPIGroup();
        }

        public TargetKPIGroup Group { get; set; }

        public List<TargetKPI> KPIs { get; set; }
    }
}
