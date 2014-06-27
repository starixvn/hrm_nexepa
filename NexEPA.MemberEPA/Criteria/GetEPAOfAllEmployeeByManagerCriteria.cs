// -----------------------------------------------------------------------
// <copyright file="GetEPAOfAllEmployeeByManagerCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.24">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get EPA of all employee by their manager criteria.
    /// </summary>
    public class GetEPAOfAllEmployeeByManagerCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int? EmployeeId { get; set; }

        public int? TargetId { get; set; }

        public int? ProjectId { get; set; }

        public int? Status { get; set; }

        public bool? IsShowAll { get; set; }

        public override string GetSettingKey()
        {
            return "EPA_GetMemberEPA";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
