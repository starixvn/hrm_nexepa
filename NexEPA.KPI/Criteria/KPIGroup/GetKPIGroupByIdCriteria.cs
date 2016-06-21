namespace NexEPA.KPI.Criteria.KPIGroup
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get KPI group by id.
    /// </summary>
    public class GetKPIGroupByIdCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_GetById";
        }

        public override bool IsValid()
        {
            return KPIGroupId > 0;
        }
    }
}
