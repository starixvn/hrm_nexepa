namespace NexEPA.Target.Models
{
    /// <summary>
    /// The EPA type model.
    /// </summary>
    public class EPATypeModel
    {
        public int EPATypeId { get; set; }

        public string Name { get; set; }

        public int TargetTypeId { get; set; }

        public int ScheduleRunType { get; set; }
    }
}
