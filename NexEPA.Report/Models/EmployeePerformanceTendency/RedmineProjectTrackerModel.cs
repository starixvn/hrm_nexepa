// --------------------------------------------------------------------------
// <copyright file="RedmineProjectTrackerModel.cs" company="Nexcel Solutions Vietnam">
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
    /// RedmineProjectTrackerModel class.
    /// </summary>
    public class RedmineProjectTrackerModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int Index { get; set; }

        public RedmineTrackerModel Tracker { get; set; }

        public IList<RedmineTrackerModel> Trackers { get; set; }
    }
}
