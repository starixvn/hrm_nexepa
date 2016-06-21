namespace NexEPA.Target.Criteria.TargetEmployeeSetting
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get target employee setting criteria.
    /// </summary>
    public class GetTargetEmployeeSettingCriteria : CriteriaBase
    {
        public int EPATypeId { get; set; }

        public int? DepartmentId { get; set; }

        public int? GroupId { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployeeSetting_Get";
        }

        public override bool IsValid()
        {
            return EPATypeId > 0;
        }
    }
}
