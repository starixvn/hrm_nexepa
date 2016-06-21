namespace NexEPA.MemberEPA.Models
{
    using System;
    using System.Collections.Generic;
    using NexEPA.Employee.Models;
    using NexEPA.Target.Models;

    /// <summary>
    /// The Appraisal Form model.
    /// </summary>
    public class AppraisalFormModel
    {
        public int EPAId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public EmployeeModel Employee { get; set; }

        public EPATypeModel EPAType { get; set; }

        public IEnumerable<EPAProcessModel> EPAProcess { get; set; }
    }
}
