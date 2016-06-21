namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update Appraisal Form criteria.
    /// </summary>
    public class UpdateAppraisalFormCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public int Status { get; set; }

        public string ProcessDetails { get; set; }

        public string EvaluateDetails { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Update";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
