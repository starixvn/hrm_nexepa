// -----------------------------------------------------------------------
// <copyright file="GetEmployeePerformanceByTeamCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.09">Created</change>
// </history>

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

        public string NickNames { get; set; }

        public int? GroupId { get; set; }

        public int? ProjectId { get; set; }

        public override bool IsValid()
        {
            return FromDate <= ToDate && !string.IsNullOrEmpty(NickNames);
        }

        public override string GetSettingKey()
        {
            return "Report_EmployeeAverageEPA";
        }
    }
}
