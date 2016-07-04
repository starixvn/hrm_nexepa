// --------------------------------------------------------------------------
// <copyright file="EditMemberCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.19">Created</change>
// </history>

namespace NexEPA.Project.Criteria.ProjectMembers
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Edit Member Criteria.
    /// </summary>
    public class EditMemberCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int? EmployeeId { get; set; }

        public string ProjectRoleIds { get; set; }

        public DateTime JoinDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return ProjectId > 0 && EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "ProjectMember_EditMemberRole";
        }
    }
}
