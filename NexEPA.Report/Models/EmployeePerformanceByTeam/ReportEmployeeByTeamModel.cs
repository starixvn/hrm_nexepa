// -----------------------------------------------------------------------
// <copyright file="ReportEmployeeByTeamModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.09">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceByTeam
{
    using System;
    using NexEPA.Core.Models;

    /// <summary>
    /// The model of employee performance by team.
    /// </summary>
    public class ReportEmployeeByTeamModel
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
