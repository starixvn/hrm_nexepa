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
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;
    using NexEPA.Target.Models;

    /// <summary>
    /// The Project History View Model.
    /// </summary>
    public class HistoryProjectViewModel
    {
        public HistoryProjectViewModel()
        {
            targetList = new List<TargetModel>();
        }

        public string Roles { get; set; }

        public decimal Score { get; set; }

        public string Comment { get; set; }

        public ProjectModel Project { get; set; }

        public EmployeeModel Evaluator { get; set; }

        public List<TargetModel> targetList { get; set; }
    }
}
