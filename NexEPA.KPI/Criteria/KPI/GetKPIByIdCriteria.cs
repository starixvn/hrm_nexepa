namespace NexEPA.KPI.Criteria.KPI
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPI by ID criteria.
    /// </summary>
    public class GetKPIByIdCriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_GetById";
        }

        public override bool IsValid()
        {
            return KPIId > 0;
        }
    }
}
