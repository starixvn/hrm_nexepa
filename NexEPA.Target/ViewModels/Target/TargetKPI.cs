// -----------------------------------------------------------------------
// <copyright file="TargetKPI.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.02">Created</change>
// </history>

namespace NexEPA.Target.ViewModels.Target
{
    /// <summary>
    /// The Target KPI Item view model.
    /// </summary>
    public class TargetKPI
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }
    }
}
