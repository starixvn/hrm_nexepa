namespace NexEPA.KPI.Criteria.KPI
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPI criteria.
    /// </summary>
    public class GetKPICriteria : CriteriaBase
    {
        public int? TargetTypeId { get; set; }

        public int? KPIGroupId { get; set; }

        public bool? IsRequireComment { get; set; }

        public bool? IsDisabled { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_Get";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
