namespace NexEPA.KPI.Model
{
    using NexEPA.Core.Models;

    /// <summary>
    /// The model of KPI Group.
    /// </summary>
    public class KPIGroupModel
    {
        public int KPIGroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? Weight { get; set; }

        public bool IsSpreadKPI { get; set; }

        public bool IsDisabled { get; set; }

        public int NumberKPI { get; set; }

        public bool Selected { get; set; }

        public int? DisplayOrder { get; set; }

        public TargetType TargetType { get; set; }
    }
}
