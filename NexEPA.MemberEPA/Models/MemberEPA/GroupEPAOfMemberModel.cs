namespace NexEPA.MemberEPA.Models.MemberEPA
{
    using System.Collections.Generic;
    using NexEPA.MemberEPA.Models.MyEPA;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GroupEPAOfMemberModel
    {
        public GroupEPAOfMemberModel()
        {
            EPAs = new List<MemberEPAModel>();
        }

        public int EmployeeId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public IList<MemberEPAModel> EPAs { get; set; }

        public int EPACount { get; set; }

        public decimal AverageScore { get; set; }
    }
}