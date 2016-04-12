namespace NexEPA.Core.Models
{
    using System;

    public class CommonEmployeeModel
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string DomainUsername { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public int ProjectId { get; set; }

        public DateTime? JoinDate { get; set; }
    }
}
