// --------------------------------------------------------------------------
// <copyright file="RedmineWorkloadByMonthModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.9">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// RedmineWorkloadByMonthModel class.
    /// </summary>
    public class RedmineWorkloadByMonthModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; }

        public decimal TotalWorkingDay { get; set; }

        public decimal Workload { get; set; }
    }
}
