// -----------------------------------------------------------------------
// <copyright file="TargetItem.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.ViewModels.Target
{
    using NexEPA.Core.Model.Target;
    using NexEPA.Target.Models.Target;

    /// <summary>
    /// The view model of Target.
    /// </summary>
    public class TargetItem
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int? Status { get; set; }

        public string StatusName { get; set; }

        public int NumberKPI { get; set; }

        public int NumberMember { get; set; }

        public TargetType TargetType { get; set; }

        public Project Project { get; set; }

        public Milestone Milestone { get; set; }
    }
}
