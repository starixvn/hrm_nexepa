// --------------------------------------------------------------------------
// <copyright file="ProjectMemberService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria.ProjectMembers;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Interface;

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

        public CommandResult DeleteMember(DeleteMemberCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get all project members.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The model of project member.</returns>
        public List<ProjectMembersModel> GetAllProjectMember(GetAllProjectMembersCriteria criteria)
        {
            var projectMembers = new List<ProjectMembersModel>();
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                var rawProjectMembers = (List<ProjectMembersModel>)this.GetData<ProjectMembersModel>(criteria);

                projectMembers = GroupProjectMembersSameRole(rawProjectMembers, projectMembers);
            }

            return projectMembers;
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

        /// <summary>
        /// Group project members have same role.
        /// </summary>
        /// <param name="rawProjectMembers">The list project members before group.</param>
        /// <param name="projectMembers">The list project members after group.</param>
        /// <returns>The list project member.</returns>
        private List<ProjectMembersModel> GroupProjectMembersSameRole(
            List<ProjectMembersModel> rawProjectMembers,
            List<ProjectMembersModel> projectMembers)
        {
            for (int i = 0; i < rawProjectMembers.Count; i++)
            {
                ProjectMembersModel projectMember = rawProjectMembers[i];

                List<ProjectMembersModel> membersSameIdDifferentRole = rawProjectMembers.Where(mem => mem.EmployeeId == projectMember.EmployeeId
                                                                && mem.Name != projectMember.Name).ToList();

                int numberMemberSameIdDifferentRole = membersSameIdDifferentRole.Count;

                if (numberMemberSameIdDifferentRole > 0)
                {
                    membersSameIdDifferentRole.ForEach(item =>
                    {
                        projectMember.Name += ", " + item.Name;
                        rawProjectMembers.Remove(item);
                    });
                }

                projectMembers.Add(projectMember);
            }

            return projectMembers;
        }
    }
}
