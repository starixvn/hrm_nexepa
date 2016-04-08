namespace NexEPA.Project.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Core.Models;
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
                result = Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult EditMember(EditMemberCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = Repository.Update(criteria);
            }

            return result;
        }

        public CommandResult DeleteMember(DeleteMemberCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = Repository.Delete(criteria);
            }

            return result;
        }

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
            IEnumerable<ProjectMembersModel> projectMembers = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                projectMembers = this.GetData<ProjectMembersModel>(criteria);
            }

            return projectMembers;
        }

        public IEnumerable<ProjectRoleModel> GetAllProjectRole(GetAllProjectRoleCriteria criteria)
        {
            IEnumerable<ProjectRoleModel> projectRoles = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                projectRoles = this.GetData<ProjectRoleModel>(criteria);
            }

            return projectRoles;
        }

        public IEnumerable<CommonEmployeeModel> GetPMByProject(GetPMByProjectCriteria criteria)
        {
            IEnumerable<CommonEmployeeModel> pms = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                pms = this.GetData<CommonEmployeeModel>(criteria);
            }

            return pms;
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

                List<ProjectMembersModel> membersSameIdDifferentRole = rawProjectMembers.Where(
                    mem => mem.EmployeeId == projectMember.EmployeeId && mem.Name != projectMember.Name).ToList();

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
