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

    /// <summary>
    /// The model of Target.
    /// </summary>
    public class TargetInfo
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TargetTypeId { get; set; }

        public int ScoreTypeId { get; set; }

        public int ProcessGroupId { get; set; }

        public int? ProjectId { get; set; }

        public int? MilestoneId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? StatusId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
