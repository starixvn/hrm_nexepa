// -----------------------------------------------------------------------
// <copyright file="GetProjectOrMilestoneDateCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.15">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.Target
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get project date or milestone date criteria.
    /// </summary>
    public class GetProjectOrMilestoneDateCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int? MilestoneId { get; set; }

        public override string GetSettingKey()
        {
            return "Target_GetDefaultDateForAddNew";
        }

        public override bool IsValid()
        {
            return ProjectId > 0;
        }
    }
}
