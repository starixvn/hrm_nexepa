namespace NexEPA.Project.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Criteria.ProjectRedmine;
    using NexEPA.Project.Models;

    /// <summary>
    /// Interface IProjectService.
    /// </summary>
    public interface IProjectService
    {
        ProjectModel GetById(GetProjectByIdCriteria criteria);

        List<ProjectInformationModel> GetAllProject(GetAllProjectCriteria criteria);

        CommandResult InsertProject(AddProjectCriteria criteria);

        CommandResult UpdateProject(UpdateProjectCriteria criteria);

        CommandResult UpdateProjectStatus(UpdateProjectStatusCriteria criteria);

        CommandResult DeleteProject(DeleteProjectCriteria criteria);

        IEnumerable<MilestoneModel> GetMilestoneByProjectId(GetMilestoneByProjectIdCriteria criteria);

        CommandResult InsertMilestone(AddMilestoneCriteria criteria);

        CommandResult UpdateMilestone(UpdateMilestoneCriteria criteria);

        CommandResult DeleteMilestone(DeleteMilestoneCriteria criteria);

        List<RedmineProjectModel> GetAllRedmineProjects(GetAllRedmineProjectsCriteria criteria);

        IList<RedmineProjectModel> GetRedmineProject(GetRedmineProjectCriteria criteria);

        CommandResult SyncProjectsFromRedmine(SyncProjectsFromRedmineCriteria criteria);

        IList<RedmineProjectModel> GetTopParentRedmineProjects(GetTopParentRedmineProjectsCriteria criteria);

        IList<RedmineProjectModel> GetSubRedmineProjects(GetSubRedmineProjectsCriteria criteria);

        string ConvertMemberListToXML(IEnumerable<MemberModel> members);

        RedmineProjectModel GetProjectRedmineById(GetProjectRedmineByIdCriteria criteria);
    }
}
