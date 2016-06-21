namespace NexEPA.Target.Criteria.Target.Target
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update target status criteria.
    /// </summary>
    public class UpdateTargetStatusCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int Status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return TargetId > 0;
        }

        public override string GetSettingKey()
        {
            return "Target_UpdateStatus";
        }
    }
}
