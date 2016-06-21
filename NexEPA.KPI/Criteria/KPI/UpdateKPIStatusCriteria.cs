namespace NexEPA.KPI.Criteria.KPI
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update status of KPI criteria.
    /// </summary>
    public class UpdateKPIStatusCriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_UpdateStatus";
        }

        public override bool IsValid()
        {
            return KPIId > 0;
        }
    }
}
