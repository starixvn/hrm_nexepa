namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Appraisal Form by Id criteria.
    /// </summary>
    public class GetAppraisalFormByIdCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_GetById";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
