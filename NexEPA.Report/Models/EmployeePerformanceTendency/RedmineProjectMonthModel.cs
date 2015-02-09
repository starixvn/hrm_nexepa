// --------------------------------------------------------------------------
// <copyright file="RedmineProjectMonthModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedmineProjectMonthModel class.
    /// </summary>
    public class RedmineProjectMonthModel
    {
        public string MonthName { get; set; }

        public decimal TotalSpentTimeOfMonth { get; set; }
    }
}
