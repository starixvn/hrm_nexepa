namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete Target Employee criteria.
    /// </summary>
    public class DeleteTargetEmployeeCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public string EmployeeIds { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_Delete";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
