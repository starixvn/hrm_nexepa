namespace NexEPA.Project.Criteria.Project
{
    using Fanex.Data.Repository;

    public class GetRedmineProjectCriteria : CriteriaBase
    {
        public int MonthPeriod { get; set; }

        public override bool IsValid()
        {
            return MonthPeriod > 0 && MonthPeriod <= 12;
        }

        public override string GetSettingKey()
        {
            return "ProjectRedmine_Get";
        }
    }
}
