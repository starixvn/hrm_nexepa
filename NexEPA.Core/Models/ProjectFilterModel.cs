namespace NexEPA.Core.Models
{
    using System;

    /// <summary>
    /// Project model use to filter.
    /// </summary>
    public class ProjectFilterModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Members { get; set; }

        public int Status { get; set; }

        public int ProjectManager { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }
    }
}
