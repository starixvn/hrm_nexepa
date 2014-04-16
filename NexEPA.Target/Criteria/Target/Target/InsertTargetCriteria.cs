// -----------------------------------------------------------------------
// <copyright file="InsertTargetCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.07">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.Target
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Insert target criteria.
    /// </summary>
    public class InsertTargetCriteria : CriteriaBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? TargetTypeId { get; set; }

        public int? ProjectId { get; set; }

        public int? MilestoneId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public override string GetSettingKey()
        {
            return "Target_Insert";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
