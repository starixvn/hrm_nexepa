namespace NexEPA.Employee.Models
{
    /// <summary>
    /// The temporary EPA model.
    /// </summary>
    public class TemporaryEPAModel
    {
        public int EmployeeId { get; set; }

        public int KPIId { get; set; }

        public string Score { get; set; }

        public int Weight { get; set; }

        public string Comment { get; set; }

        public int KPIGroupId { get; set; }

        public int KPIGroupWeight { get; set; }

        public bool IsSpreadKPI { get; set; }

        public int Status { get; set; }
    }
}
