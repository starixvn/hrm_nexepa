namespace NexEPA.Target.Criteria.Target.TargetKPI
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPI Group by TargetId.
    /// </summary>
    public class GetKPIGroupByTargetIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "Target_GetKPIGroup";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
