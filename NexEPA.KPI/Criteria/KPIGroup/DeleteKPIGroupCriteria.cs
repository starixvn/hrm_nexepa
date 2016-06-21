namespace NexEPA.KPI.Criteria.KPIGroup
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete KPI group criteria.
    /// </summary>
    public class DeleteKPIGroupCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_Delete";
        }

        public override bool IsValid()
        {
            return KPIGroupId > 0;
        }
    }
}
