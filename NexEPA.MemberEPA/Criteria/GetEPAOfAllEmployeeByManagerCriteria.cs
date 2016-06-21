namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Get EPA of all employee by their manager criteria.
    /// </summary>
    public class GetEPAOfAllEmployeeByManagerCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string EmployeeId { get; set; }

        public int? TeamId { get; set; }

        public int? TargetId { get; set; }

        public int? ProjectId { get; set; }

        public int? Status { get; set; }

        public bool? IsShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "EPA_GetMemberEPA";
        }

        public override bool IsValid()
        {
            if (ToDate != null)
            {
                return FromDate <= ToDate;
            }

            return true;
        }
    }
}