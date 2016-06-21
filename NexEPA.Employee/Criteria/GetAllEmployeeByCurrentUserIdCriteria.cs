namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get employees by current user Id.
    /// </summary>
    public class GetAllEmployeeByCurrentUserIdCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public string Keyword { get; set; }

        public string ExcludedGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_GetMembers";
        }

        public override bool IsValid()
        {
             return ActorUserId > 0;
        }
    }
}
