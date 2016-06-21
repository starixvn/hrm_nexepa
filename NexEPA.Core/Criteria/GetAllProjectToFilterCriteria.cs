namespace NexEPA.Core.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all project to filter criteria.
    /// </summary>
    public class GetAllProjectToFilterCriteria : CriteriaBase
    {
        public bool? IsShowClosed { get; set; }

        public int ActorUserId { get; set; }

        public int? IncludeProjectId { get; set; }

        public override string GetSettingKey()
        {
            return "Project_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
