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
            TargetList = new List<TargetModel>();
        }

        public string Roles { get; set; }

        public decimal Score { get; set; }

        public string Comment { get; set; }

        public ProjectModel Project { get; set; }

        public EmployeeModel Evaluator { get; set; }

        public List<TargetModel> TargetList { get; set; }
    }
}
