namespace NexEPA.Target.Models
{
    using System;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// The model of Target.
    /// </summary>
    public class TargetModel
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public int ScoreTypeId { get; set; }

        public int ProcessGroupId { get; set; }

        public int KPIs { get; set; }

        public int Members { get; set; }

        public int TotalOpenEPA { get; set; }

        public int TotalEPA { get; set; }

        public string Score { get; set; }

        public int Index { get; set; }

        public TargetType TargetType { get; set; }

        public ProjectModel Project { get; set; }

        public MilestoneModel Milestone { get; set; }

        public EmployeeModel ProjectManager { get; set; }
    }
}
