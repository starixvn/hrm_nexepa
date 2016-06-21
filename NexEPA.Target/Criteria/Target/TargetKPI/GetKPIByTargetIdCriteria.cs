namespace NexEPA.Target.Criteria.Target.TargetKPI
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPIs by Target ID criteria.
    /// </summary>
    public class GetKPIByTargetIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int? KPIGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_GetByTarget";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
