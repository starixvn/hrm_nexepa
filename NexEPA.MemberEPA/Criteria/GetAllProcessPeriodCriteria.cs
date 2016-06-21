namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all process period criteria.
    /// </summary>
    public class GetAllProcessPeriodCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "ProcessPeriod_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
