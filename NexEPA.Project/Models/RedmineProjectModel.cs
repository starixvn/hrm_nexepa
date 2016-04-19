namespace NexEPA.Project.Models
{
    using System;
    using System.Collections.Generic;
    using NexEPA.Core.Models;

    /// <summary>
    /// RedmineProjectModel class.
    /// </summary>
    public class RedmineProjectModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int? ParentId { get; set; }

        public int Status { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? SyncedDate { get; set; }

        public int ProjectManagerId { get; set; }

        public List<CommonEmployeeModel> ProjectManagers { get; set; }

        public List<RedmineProjectModel> SubProjects { get; set; }

        public decimal TotalSpentTimeOfAllMonths { get; set; }

        public int IssueCount { get; set; }
    }
}
