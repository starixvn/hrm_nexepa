namespace NexEPA.KPI.Criteria.KPIGroup
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update status of KPI Group criteria.
    /// </summary>
    public class UpdateKPIGroupStatusCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_UpdateStatus";
        }

        public override bool IsValid()
        {
            return KPIGroupId > 0;
        }
    }
}
