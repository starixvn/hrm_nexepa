// --------------------------------------------------------------------------
// <copyright file="RedminePersonalEffortDetailModel.cs" company="Nexcel Solutions Vietnam">
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
    /// RedminePersonalEffortDetailModel class.
    /// </summary>
    public class RedminePersonalEffortDetailModel
    {
        public int ProjectId { get; set; }

        public int TrackerId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int NumberIssue { get; set; }

        public decimal SpentTime { get; set; }
    }
}
