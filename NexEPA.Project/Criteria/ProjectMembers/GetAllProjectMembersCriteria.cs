namespace NexEPA.Project.Criteria.ProjectMembers
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get All Project Members Criteria.
    /// </summary>
    public class GetAllProjectMembersCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int? MilestoneId { get; set; }

        public bool? IsShowDeletedMember { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectMember_Get";
        }
    }
}
