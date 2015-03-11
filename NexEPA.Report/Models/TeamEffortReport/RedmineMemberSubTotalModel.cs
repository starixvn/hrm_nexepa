// --------------------------------------------------------------------------
// <copyright file="RedmineMemberSubTotalModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    /// RedmineMemberSubTotalModel class.
    /// </summary>
    public class RedmineMemberSubTotalModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineYearMonthModel YearMonth { get; set; }

        public decimal SpentTime { get; set; }        
    }
}
