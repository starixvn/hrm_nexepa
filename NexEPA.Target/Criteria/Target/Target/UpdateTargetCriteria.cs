namespace NexEPA.Target.Criteria.Target.Target
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update Target Criteria.
    /// </summary>
    public class UpdateTargetCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? TargetTypeId { get; set; }

        public int? ProjectId { get; set; }

        public int? ProjectManager { get; set; }

        public int? MilestoneId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "Target_Update";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
