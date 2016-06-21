namespace NexEPA.Target.Criteria.Target.Target
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get target to filter.
    /// </summary>
    public class GetTargetToFilterCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int? Status { get; set; }

        public override string GetSettingKey()
        {
            return "Target_GetAll";
        }

        public override bool IsValid()
        {
            return ActorUserId > 0;
        }
    }
}
