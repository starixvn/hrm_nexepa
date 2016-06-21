namespace NexEPA.Core.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all Target Type.
    /// </summary>
    public class GetAllTargetTypeCriteria : CriteriaBase
    {
        public bool? IsDisabled { get; set; }

        [SpParam(SqlParamName = "IsHasGetAll")]
        public bool HasGetAll { get; set; }

        public override string GetSettingKey()
        {
            return "TargetType_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
