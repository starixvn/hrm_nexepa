// --------------------------------------------------------------------------
// <copyright file="RedmineTeamTotalEffortModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.Models.TeamEffortReport
{
    using NexEPA.Core.Models;

    /// <summary>
    /// RedmineTeamTotalEffortModel class.
    /// </summary>
    public class RedmineTeamTotalEffortModel
    {
        public RedmineYearMonthModel YearMonth { get; set; }

        public decimal Total { get; set; }
    }
}
