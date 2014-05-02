// --------------------------------------------------------------------------
// <copyright file="DeleteMemberCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Criteria.ProjectMembers
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Delete Member Criteria.
    /// </summary>
    public class DeleteMemberCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectMember_DeleteMember";
        }
    }
}
