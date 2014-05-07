// -----------------------------------------------------------------------
// <copyright file="TargetKPIInfo.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.17">Created</change>
// </history>

namespace NexEPA.Target.Models.Target
{
    using NexEPA.Core.Models;

    /// <summary>
    /// Target KPI Info model.
    /// </summary>
    public class TargetKPIInfo
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public string Description { get; set; }

        public bool IsRequireComment { get; set; }

        public bool IsDisabled { get; set; }

        public TargetKPIGroup Group { get; set; }

        public TargetType TargetType { get; set; }
    }
}
