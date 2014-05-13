// --------------------------------------------------------------------------
// <copyright file="IProjectMemberService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Services.Interface
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Criteria.ProjectMembers;
    using NexEPA.Project.Models;

    /// <summary>
    /// Interface Project Member Service.
    /// </summary>
    public interface IProjectMemberService
    {
        CommandResult AddMember(AddMemberCriteria criteria);

        CommandResult DeleteMember(DeleteMemberCriteria criteria);

        List<ProjectMembersModel> GetAllProjectMember(GetAllProjectMembersCriteria criteria);

        IEnumerable<ProjectMembersModel> GetMemberRoleInProject(GetMemberRoleInProjectCriteria criteria);

        IEnumerable<ProjectRoleModel> GetAllProjectRole(GetAllProjectRoleCriteria criteria);
    }
}
