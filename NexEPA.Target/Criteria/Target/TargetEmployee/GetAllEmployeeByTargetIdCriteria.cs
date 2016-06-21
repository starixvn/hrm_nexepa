namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all Employee by Target Id criteria.
    /// </summary>
    public class GetAllEmployeeByTargetIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_GetAll";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
