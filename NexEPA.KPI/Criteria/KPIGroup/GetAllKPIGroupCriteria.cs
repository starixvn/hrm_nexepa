namespace NexEPA.KPI.Criteria.KPIGroup
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all KPI group criteria.
    /// </summary>
    public class GetAllKPIGroupCriteria : CriteriaBase
    {
        public bool? IsDisabled { get; set; }

        public int? TargetTypeId { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
