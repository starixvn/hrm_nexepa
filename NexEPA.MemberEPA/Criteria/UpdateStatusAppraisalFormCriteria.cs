namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update status of Appraisal Form criteria.
    /// </summary>
    public class UpdateStatusAppraisalFormCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public int Status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_UpdateStatus";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
