namespace NexEPA.Report.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Get employee performance by team criteria.
    /// </summary>
    public class GetEmployeePerformanceByTeamCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string EmployeeId { get; set; }

        public int? GroupId { get; set; }

        public int? ProjectId { get; set; }

        public override bool IsValid()
        {
            return FromDate <= ToDate && !string.IsNullOrEmpty(EmployeeId);
        }

        public override string GetSettingKey()
        {
            return "Report_EmployeeAverageEPA";
        }
    }
}
