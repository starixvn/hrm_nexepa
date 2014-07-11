// --------------------------------------------------------------------------
// <copyright file="AddMemberCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Criteria.ProjectMembers
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Member Criteria.
    /// </summary>
    public class AddMemberCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int? EmployeeId { get; set; }

        public string ProjectRoleIds { get; set; }

        public DateTime? JoinDate { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectMember_AddMemberRole";
        }
    }
}
