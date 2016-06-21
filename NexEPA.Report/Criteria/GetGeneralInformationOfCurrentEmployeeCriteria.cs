namespace NexEPA.Report.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get general information of current employee that used in dashboard.
    /// </summary>
    public class GetGeneralInformationOfCurrentEmployeeCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "Report_MySpace";
        }
    }
}
