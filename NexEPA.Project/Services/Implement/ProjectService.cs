namespace NexEPA.Project.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Common.Helper;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Criteria.ProjectRedmine;
    using NexEPA.Project.Models;
    using NexEPA.Project.Repositories.Implement;
    using NexEPA.Project.Repositories.Interface;
    using NexEPA.Project.Services.Interface;

    /// <summary>
    /// Project Service.
    /// </summary>
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository = null)
        {
            this._projectRepository = projectRepository ?? new ProjectRepository();
        }

        /// <summary>
        /// Get Project By id.
        /// </summary>
        /// <param name="criteria">Get Project By Id Criteria.</param>
        /// <returns>Project Model.</returns>
        public ProjectModel GetById(GetProjectByIdCriteria criteria)
        {
            ProjectModel result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.GetDataEntity<ProjectModel>(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get all project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>List Project Model.</returns>
        public List<ProjectInformationModel> GetAllProject(GetAllProjectCriteria criteria)
        {
            List<ProjectInformationModel> projects = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                projects = _projectRepository.GetAllProject(criteria);
            }

            return projects;
        }

        /// <summary>
        /// Gets the redmine projects of users.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The redmine projects of users.</returns>
        public List<RedmineProjectModel> GetAllRedmineProjects(GetAllRedmineProjectsCriteria criteria)
        {
            List<RedmineProjectModel> projects = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                projects = GetData<RedmineProjectModel>(criteria).ToList();
            }

            return projects;
        }

        public RedmineProjectModel GetProjectRedmineById(GetProjectRedmineByIdCriteria criteria)
        {
            RedmineProjectModel project = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                project = GetDataEntity<RedmineProjectModel>(criteria);
            }

            return project;
        }

        /// <summary>
        /// Insert Project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult InsertProject(AddProjectCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Insert(criteria);
            }

            return result;
        }

        /// <summary>
        /// Update Project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateProject(UpdateProjectCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Update Project Status.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateProjectStatus(UpdateProjectStatusCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get Milestone By Project Id.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>List Milestone.</returns>
        public IEnumerable<MilestoneModel> GetMilestoneByProjectId(GetMilestoneByProjectIdCriteria criteria)
        {
            IEnumerable<MilestoneModel> milestones = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                milestones = this.GetData<MilestoneModel>(criteria);
            }

            return milestones;
        }

        /// <summary>
        /// Update Milestone.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateMilestone(UpdateMilestoneCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Insert Milestone.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult InsertMilestone(AddMilestoneCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Insert(criteria);
            }

            return result;
        }

        /// <summary>
        /// Delete Milestone.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult DeleteMilestone(DeleteMilestoneCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Delete(criteria);
            }

            return result;
        }

        /// <summary>
        /// Delete Project.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult DeleteProject(DeleteProjectCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Delete(criteria);
            }

            return result;
        }

        public IList<RedmineProjectModel> GetRedmineProject(GetRedmineProjectCriteria criteria)
        {
            IList<RedmineProjectModel> projects = new List<RedmineProjectModel>();
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                projects = _projectRepository.GetRedmineProject(criteria);
            }

            return projects;
        }

        public CommandResult SyncProjectsFromRedmine(SyncProjectsFromRedmineCriteria criteria)
        {
            var result = new CommandResult();
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                result = _projectRepository.SyncProjectsFromRedmine(criteria);
            }

            return result;
        }

        public IList<RedmineProjectModel> GetTopParentRedmineProjects(GetTopParentRedmineProjectsCriteria criteria)
        {
            IList<RedmineProjectModel> result = new List<RedmineProjectModel>();
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                result = GetData<RedmineProjectModel>(criteria).ToList();
            }

            return result;
        }

        public IList<RedmineProjectModel> GetSubRedmineProjects(GetSubRedmineProjectsCriteria criteria)
        {
            IList<RedmineProjectModel> result = new List<RedmineProjectModel>();
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                result = GetData<RedmineProjectModel>(criteria).ToList();
            }

            return result;
        }

        public string ConvertMemberListToXML(IEnumerable<ProjectMemberRoleModel> members)
        {
            StringBuilder epaPattern = new StringBuilder(Constants.BeginRootTag);

            foreach (ProjectMemberRoleModel member in members)
            {
                foreach (ProjectRoleModel role in member.Roles)
                {
                    epaPattern.AppendFormat(Constants.MembersList, member.Member.EmployeeId, member.Member.JoinDate, role.ProjectRoleId);
                }
            }

            epaPattern.Append(Constants.EndRootTag);
            return epaPattern.ToString();
        }
    }
}
