// --------------------------------------------------------------------------
// <copyright file="GetMemberRoleInProjectCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Get Member Role In Project Criteria.
    /// </summary>
    public class GetMemberRoleInProjectCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectMember_GetMemberRole";
        }
    }
}
