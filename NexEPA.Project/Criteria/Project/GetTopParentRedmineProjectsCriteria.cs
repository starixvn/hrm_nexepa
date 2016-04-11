namespace NexEPA.Project.Criteria.Project
{
    using Fanex.Data.Repository;

    public class GetTopParentRedmineProjectsCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectRedmine_GetTopParent";
        }
    }
}
