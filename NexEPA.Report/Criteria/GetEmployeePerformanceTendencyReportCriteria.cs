// -----------------------------------------------------------------------
// <copyright file="GetEmployeePerformanceTendencyReportCriteria.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.08">Created</change>
// </history>

namespace NexEPA.Report.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get employee performance tendency report criteria.
    /// </summary>
    public class GetEmployeePerformanceTendencyReportCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public int ActorUserId { get; set; }

        public override bool IsValid()
        {
            return EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "Report_EmployeeEPA";
        }
    }
}
