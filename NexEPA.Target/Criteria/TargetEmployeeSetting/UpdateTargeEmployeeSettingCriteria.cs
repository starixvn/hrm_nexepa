namespace NexEPA.Target.Criteria.TargetEmployeeSetting
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update Target Template Setting criteria.
    /// </summary>
    public class UpdateTargeEmployeeSettingCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public int EPATypeId { get; set; }

        public int? TargetTemplateId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployeeSetting_Update";
        }

        public override bool IsValid()
        {
            return EmployeeId > 0 && EPATypeId > 0;
        }
    }
}
