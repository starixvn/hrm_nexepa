// -----------------------------------------------------------------------
// <copyright file="KPIGroupInfo.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.17">Created</change>
// </history>

namespace NexEPA.KPI.Model
{
    using System;
    using NexEPA.Core.Models;

    /// <summary>
    /// The model of KPI Group.
    /// </summary>
    public class KPIGroupInfo
    {
        public int KPIGroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public bool IsDisabled { get; set; }

        public int NumberKPI { get; set; }

        public TargetType TargetType { get; set; }
    }
}
