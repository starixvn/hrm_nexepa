// --------------------------------------------------------------------------
// <copyright file="RedmineProjectModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    /// <summary>
    /// RedmineProjectModel class.
    /// </summary>
    public class RedmineProjectModel
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public decimal TotalSpentTimeOfAllMonths { get; set; }

        public int IssueCount { get; set; }
    }
}
