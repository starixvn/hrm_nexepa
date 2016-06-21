namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete Appraisal Form criteria.
    /// </summary>
    public class DeleteAppraisalFormCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Delete";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
