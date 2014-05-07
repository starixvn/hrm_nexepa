// -----------------------------------------------------------------------
// <copyright file="TargetInfo.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Models.Target
{
    using System;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;

    /// <summary>
    /// The model of Target.
    /// </summary>
    public class TargetInfo
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Status { get; set; }

        public int ScoreTypeId { get; set; }

        public int ProcessGroupId { get; set; }

        public int KPIs { get; set; }

        public int Members { get; set; }

        public TargetType TargetType { get; set; }

        public Project Project { get; set; }

        public Milestone Milestone { get; set; }
    }
}
