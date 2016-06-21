namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get employee by KPI criteria.
    /// </summary>
    public class GetEmployeeByKPICriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int KPIId { get; set; }

        public int KPIGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_GetByKPI";
        }

        public override bool IsValid()
        {
            return TargetId > 0 && KPIId > 0 && KPIGroupId > 0;
        }
    }
}
