// -----------------------------------------------------------------------
// <copyright file="TeamPerformanceModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.09">Created</change>
// </history>

namespace NexEPA.Report.Models.TeamEPAReport
{
    using System.Collections.Generic;

    /// <summary>
    /// The model of employee performance by team report.
    /// </summary>
    public class TeamPerformanceModel
    {
        public IEnumerable<EmployeePerformanceByProjectModel> ReportByProject { get; set; }
    }
}
