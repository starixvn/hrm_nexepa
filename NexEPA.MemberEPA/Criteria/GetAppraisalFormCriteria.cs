namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get appraisal form criteria.
    /// </summary>
    public class GetAppraisalFormCriteria : CriteriaBase
    {
        public int? EPATypeId { get; set; }

        public string Keyword { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Get";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
