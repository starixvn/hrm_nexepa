namespace NexEPA.Project.Criteria.Project
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Project Criteria.
    /// </summary>
    public class AddProjectCriteria : CriteriaBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ProjectManager { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public byte? Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string MemberList { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Project_Insert";
        }
    }
}
