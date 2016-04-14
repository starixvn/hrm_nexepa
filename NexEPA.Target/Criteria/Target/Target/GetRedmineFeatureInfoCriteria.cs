using Fanex.Data.Repository;

namespace NexEPA.Target.Criteria.Target
{
    public class GetRedmineFeatureInfoCriteria : CriteriaBase
    {
        public int RedmineFeatureId { get; set; }

        public override bool IsValid()
        {
            return RedmineFeatureId > 0;
        }

        public override string GetSettingKey()
        {
            return "TargetRedmine_GetFeatureInfo";
        }
    }
}
