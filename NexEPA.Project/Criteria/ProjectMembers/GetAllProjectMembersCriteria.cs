// --------------------------------------------------------------------------
// <copyright file="GetAllProjectMembersCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Criteria.ProjectMembers
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get All Project Members Criteria.
    /// </summary>
    public class GetAllProjectMembersCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int? MilestoneId { get; set; }

        public bool? IsDisabled { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectMember_Get";
        }
    }
}
