// --------------------------------------------------------------------------
// <copyright file="RedmineIssueModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Core.Models
{
    using System;

    /// <summary>
    /// RedmineIssueModel class.
    /// </summary>
    public class RedmineIssueModel
    {
        public int IssueId { get; set; }

        public string IssueName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
