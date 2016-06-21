namespace NexEPA.MemberEPA.Models
{
    using System;
    using System.Collections.Generic;
    using NexEPA.Employee.Models;

    /// <summary>
    /// The EPA Process model.
    /// </summary>
    public class EPAProcessModel
    {
        public EPAProcessModel()
        {
            EvaluatorList = new List<EmployeeModel>();
        }

        public int ProcessId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public bool IsSelfEvaluated { get; set; }

        public bool NoticeAtBegin { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime RemindDate { get; set; }

        public EmployeeModel Evaluator { get; set; }

        public List<EmployeeModel> EvaluatorList { get; set; }
    }
}
