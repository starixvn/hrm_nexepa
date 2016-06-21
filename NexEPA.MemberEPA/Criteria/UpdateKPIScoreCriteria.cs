namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update KPI Score criteria.
    /// </summary>
    public class UpdateKPIScoreCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int KPIId { get; set; }

        public int KPIGroupId { get; set; }

        public string EmployeeScore { get; set; }

        public string Comments { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPADetail_UpdateKPIScore";
        }

        public override bool IsValid()
        {
            return TargetId > 0 && KPIId > 0 && KPIGroupId > 0;
        }
    }
}
