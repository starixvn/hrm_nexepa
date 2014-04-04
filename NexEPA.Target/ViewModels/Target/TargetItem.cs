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
    using System;

    /// <summary>
    /// The view model of Target.
    /// </summary>
    public class TargetItem
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TargetTypeId { get; set; }

        public string TargetTypeName { get; set; }

        public int? ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int? MilestoneId { get; set; }

        public string MilestoneName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int? StatusId { get; set; }

        public string Status { get; set; }
    }
}
