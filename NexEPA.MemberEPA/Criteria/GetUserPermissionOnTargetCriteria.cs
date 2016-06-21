namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get user permission on target criteria.
    /// </summary>
    public class GetUserPermissionOnTargetCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "EPADetail_CheckPermission";
        }

        public override bool IsValid()
        {
            return ActorUserId > 0 && TargetId > 0;
        }
    }
}
