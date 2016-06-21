namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Target Employee criteria.
    /// </summary>
    public class AddTargetEmployeeCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_Add";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
