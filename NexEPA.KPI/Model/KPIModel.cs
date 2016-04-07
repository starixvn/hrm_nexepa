namespace NexEPA.KPI.Model
{
    using NexEPA.Core.Models;

    /// <summary>
    /// The model of KPI.
    /// </summary>
    public class KPIModel
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasComment { get; set; }

        public bool IsRequireComment { get; set; }

        public bool IsDisabled { get; set; }

        public string Score { get; set; }

        public bool? IsAllRating { get; set; }

        public int Percent { get; set; }

        public string Comment { get; set; }

        public int Weight { get; set; }

        public KPIGroupModel Group { get; set; }

        public TargetType TargetType { get; set; }

        public bool CheckNullComment { get; set; }

        public int EvaluateTime { get; set; }

        public decimal AverageScore { get; set; }
    }
}
