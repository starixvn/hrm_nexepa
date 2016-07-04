// --------------------------------------------------------------------------
// <copyright file="RedmineMemberWorkloadModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.Models.TeamEffortReport
{
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;

    /// <summary>
    /// RedmineMemberWorkloadModel class.
    /// </summary>
    public class RedmineMemberWorkloadModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineYearMonthModel YearMonth { get; set; }

        public decimal Workload { get; set; }
    }
}
