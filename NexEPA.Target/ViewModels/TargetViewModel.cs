// -----------------------------------------------------------------------
// <copyright file="TargetViewModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
//   <change who="Bradley.Mac" date="2014.5.13">Use common Model</change>
// </history>

namespace NexEPA.Target.ViewModels
{
    using NexEPA.Core.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// The view model of Target.
    /// </summary>
    public class TargetViewModel
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

        public ProjectModel Project { get; set; }

        public MilestoneModel Milestone { get; set; }
    }
}
