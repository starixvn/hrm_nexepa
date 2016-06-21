namespace NexEPA.MemberEPA.Models
{
    /// <summary>
    /// Process Period model.
    /// </summary>
    public class ProcessPeriodModel
    {
        public int ProcessId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public bool IsSelfEvaluated { get; set; }
    }
}
