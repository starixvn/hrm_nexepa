// -----------------------------------------------------------------------
// <copyright file="MilestoneModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    using System;

    /// <summary>
    /// The Milestone Model.
    /// </summary>
    public class MilestoneModel
    {
        public int TargetId { get; set; }

        public int? MilestoneId { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Score { get; set; }

        public string Comment { get; set; }
     }
}
