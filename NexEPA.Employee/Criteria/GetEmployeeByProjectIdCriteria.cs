namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get employee by project ID criteria.
    /// </summary>
    public class GetEmployeeByProjectIdCriteria : CriteriaBase
    {
        public int? ProjectId { get; set; }

        public string Keyword { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_GetByProject";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
