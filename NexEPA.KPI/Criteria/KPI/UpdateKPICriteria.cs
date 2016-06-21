namespace NexEPA.KPI.Criteria.KPI
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update KPI criteria.
    /// </summary>
    public class UpdateKPICriteria : CriteriaBase
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string KPIGroupIds { get; set; }

        public bool HasComment { get; set; }

        public bool IsRequireComment { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPI_Update";
        }

        public override bool IsValid()
        {
            return KPIId > 0 && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(KPIGroupIds);
        }
    }
}
