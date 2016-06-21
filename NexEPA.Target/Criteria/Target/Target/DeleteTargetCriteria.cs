namespace NexEPA.Target.Criteria.Target.Target
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete Target criteria.
    /// </summary>
    public class DeleteTargetCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "Target_Delete";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
