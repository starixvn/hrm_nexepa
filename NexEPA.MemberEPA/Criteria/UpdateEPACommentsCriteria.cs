namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    public class UpdateEPACommentsCriteria : CriteriaBase
    {
        public string Comments { get; set; }

        public override string GetSettingKey()
        {
            return "EPAProcess_UpdateComment";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
