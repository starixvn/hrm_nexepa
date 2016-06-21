namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The Target KPI Group Item model.
    /// </summary>
    public class TargetKPIGroupModel
    {
        public TargetKPIGroupModel()
        {
            KPIs = new List<KPIModel>();
            Group = new KPIGroupModel();
        }

        public KPIGroupModel Group { get; set; }

        public List<KPIModel> KPIs { get; set; }
    }
}
