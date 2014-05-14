// -----------------------------------------------------------------------
// <copyright file="ReportEmployeePerformanceByTeamEvaluatedByProjectModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.14">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceByTeam
{
    using System;
    using NexEPA.Core.Models;

    /// <summary>
    /// The model of Employee Performance by Team that evaluated by project.
    /// </summary>
    public class ReportEmployeePerformanceByTeamEvaluatedByProjectModel
    {
        public int EmployeeId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public DateTime JoinedDate { get; set; }

        public int ProjectCount { get; set; }

        public int EPACount { get; set; }

        public decimal AverageScore { get; set; }

        public Group Group { get; set; }
    }
}
