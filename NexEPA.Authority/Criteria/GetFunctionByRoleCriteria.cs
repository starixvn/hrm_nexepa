namespace NexEPA.Authority.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get function by role criteria.
    /// </summary>
    public class GetFunctionByRoleCriteria : CriteriaBase
    {
        public int RoleId { get; set; }

        public override string GetSettingKey()
        {
            return "Function_GetByRole";
        }

        public override bool IsValid()
        {
            return RoleId > 0;
        }
    }
}
