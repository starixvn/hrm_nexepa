namespace NexEPA.Core.Models
{
    /// <summary>
    /// The Group Info model.
    /// </summary>
    public class Group
    {
        public int GroupId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }
    }
}
