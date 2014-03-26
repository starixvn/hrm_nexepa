// --------------------------------------------------------------------------
// <copyright file="ProjectMemberService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Services.Impl
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria.ProjectMembers;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Project Member Service.
    /// </summary>
    public class ProjectMemberService : BaseService, IProjectMemberService
    {
        public CommandResult AddMember(AddMemberCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult DeleteMemebr(DeleteMemberCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }

        public IEnumerable<ProjectMembersModel> GetAllProjectMember(GetAllProjectMembersCriteria criteria)
        {
            IEnumerable<ProjectMembersModel> lstProjectMembers = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                lstProjectMembers = this.GetData<ProjectMembersModel>(criteria);
            }

            return lstProjectMembers;
        }

        public IEnumerable<ProjectMembersModel> GetMemberRoleInProject(GetMemberRoleInProjectCriteria criteria)
        {
            IEnumerable<ProjectMembersModel> lstProjectMembers = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                lstProjectMembers = this.GetData<ProjectMembersModel>(criteria);
            }

            return lstProjectMembers;
        }

        public IEnumerable<ProjectRoleModel> GetAllProjectRole(GetAllProjectRoleCriteria criteria)
        {
            IEnumerable<ProjectRoleModel> lstProjectRole = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                lstProjectRole = this.GetData<ProjectRoleModel>(criteria);
            }

            return lstProjectRole;
        }
    }
}
