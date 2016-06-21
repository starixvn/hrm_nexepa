namespace NexEPA.Authority.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update role criteria.
    /// </summary>
    public class UpdateRoleOfEmployeeCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public string RoleIds { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_UpdateRole";
        }

        public override bool IsValid()
        {
            return EmployeeId > 0;
        }
    }
}
