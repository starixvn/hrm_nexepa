namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Insert Appraisal Form criteria.
    /// </summary>
    public class InsertAppraisalFormCriteria : CriteriaBase
    {
        public int EPATypeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int EmployeeId { get; set; }

        public string ProcessDetails { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Insert";
        }

        public override bool IsValid()
        {
            return EPATypeId > 0 && EmployeeId > 0;
        }
    }
}
