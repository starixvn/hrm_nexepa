namespace NexEPA.MemberEPA.Models.MyEPA
{
    using System;

    /// <summary>
    /// EPA of member model.
    /// </summary>
    public class MemberEPAModel
    {
        public int EmployeeId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public string Score { get; set; }

        public string Comment { get; set; }

        public DateTime EvaluateDate { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public int Permission { get; set; }

        public Target Target { get; set; }

        public Project Project { get; set; }

        public Milestone Milestone { get; set; }

        public Evaluator Evaluator { get; set; }
    }
}
