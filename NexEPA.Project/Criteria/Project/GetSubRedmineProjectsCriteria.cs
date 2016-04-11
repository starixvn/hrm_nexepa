namespace NexEPA.Project.Criteria.Project
{
    using Fanex.Data.Repository;

    public class GetSubRedmineProjectsCriteria : CriteriaBase
    {
        public int? ParentId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectRedmine_GetSubProjects";
        }
    }
}
