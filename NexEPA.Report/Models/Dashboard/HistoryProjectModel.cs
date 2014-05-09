// -----------------------------------------------------------------------
// <copyright file="HistoryProjectModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    using System;

    /// <summary>
    /// The Project History Model.
    /// </summary>
    public class HistoryProjectModel
    {
        public int TargetId { get; set; }

        public string Roles { get; set; }

        public decimal Score { get; set; }

        public string Comment { get; set; }

        public ProjectModel Project { get; set; }

        public EvaluatorModel Evaluator { get; set; }

        public MilestoneModel Milestone { get; set; }
    }
}
