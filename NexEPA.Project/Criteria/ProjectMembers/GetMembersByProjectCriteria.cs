namespace NexEPA.Project.Criteria.ProjectMembers
{
    using Fanex.Data.Repository;

    public class GetMembersByProjectCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public override bool IsValid()
        {
            return ProjectId > 0;
        }

        public override string GetSettingKey()
        {
            return "ProjectMemberRedmine_GetByProject";
        }
    }
}
