namespace NexEPA.Target.Criteria.TargetEmployeeSetting
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get EPA type criteria.
    /// </summary>
    public class GetEPATypeCriteria : CriteriaBase
    {
        public int? EPATypeId { get; set; }

        public override string GetSettingKey()
        {
            return "EPAType_Get";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
