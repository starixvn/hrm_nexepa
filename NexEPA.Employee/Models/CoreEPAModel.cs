namespace NexEPA.Employee.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The core EPA model.
    /// </summary>
    public class CoreEPAModel
    {
        public CoreEPAModel()
        {
            KPIItems = new List<KPIModel>();
        }

        public int EPAId { get; set; }

        public string Comment { get; set; }

        public int KPIGroupId { get; set; }

        public int KPIGroupWeight { get; set; }

        public bool IsSpreadKPI { get; set; }

        public int Status { get; set; }

        public List<KPIModel> KPIItems { get; set; }
    }
}