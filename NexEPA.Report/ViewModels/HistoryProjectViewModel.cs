// -----------------------------------------------------------------------
// <copyright file="HistoryProjectViewModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.Report.Models.Dashboard;

    /// <summary>
    /// The Project History View Model.
    /// </summary>
    public class HistoryProjectViewModel
    {
        public int TargetId { get; set; }

        public string Roles { get; set; }

        public int Score { get; set; }

        public ProjectModel Project { get; set; }

        public EvaluatorModel Evaluator { get; set; }

        public List<MilestoneModel> Milestones { get; set; }
    }
}
