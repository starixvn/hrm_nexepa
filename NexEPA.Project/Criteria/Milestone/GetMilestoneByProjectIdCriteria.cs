// --------------------------------------------------------------------------
// <copyright file="GetMilestoneByProjectIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Criteria.Milestone
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Milestone By Project Id Criteria.
    /// </summary>
    public class GetMilestoneByProjectIdCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? Status { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Milestone_GetByProject";
        }
    }
}
