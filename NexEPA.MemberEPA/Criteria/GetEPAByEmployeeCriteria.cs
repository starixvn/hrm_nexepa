namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get EPA by employee criteria.
    /// </summary>
    public class GetEPAByEmployeeCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public int? ProjectId { get; set; }

        public int? Status { get; set; }

        public override string GetSettingKey()
        {
            return "EPA_GetByEmployee";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
