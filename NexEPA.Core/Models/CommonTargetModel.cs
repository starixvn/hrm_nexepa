namespace NexEPA.Core.Models
{
    using System;

    public class CommonTargetModel
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? SyncedDate { get; set; }

        public int? RedmineFeatureId { get; set; }

        public int ProjectId { get; set; }
    }
}
