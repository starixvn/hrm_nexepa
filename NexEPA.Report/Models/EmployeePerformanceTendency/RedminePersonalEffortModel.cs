// --------------------------------------------------------------------------
// <copyright file="RedminePersonalEffortModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendency
{
    /// <summary>
    /// RedminePersonalEffortModel class.
    /// </summary>
    public class RedminePersonalEffortModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string MonthName { get; set; }

        public decimal SpentTime { get; set; }
    }
}
