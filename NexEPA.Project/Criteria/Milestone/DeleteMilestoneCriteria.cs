// --------------------------------------------------------------------------
// <copyright file="DeleteMilestoneCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Criteria.Milestone
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete Milestone Criteria.
    /// </summary>
    public class DeleteMilestoneCriteria : CriteriaBase
    {
        public int MilestoneId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Milestone_Delete";
        }
    }
}
