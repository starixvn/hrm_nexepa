namespace NexEPA.KPI.Criteria.KPIGroup
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update KPI group criteria.
    /// </summary>
    public class UpdateKPIGroupCriteria : CriteriaBase
    {
        public int KPIGroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public short TargetTypeId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "KPIGroup_Update";
        }

        public override bool IsValid()
        {
            return KPIGroupId > 0 && TargetTypeId >= 0 && !string.IsNullOrWhiteSpace(Name);
        }
    }
}
