// -----------------------------------------------------------------------
// <copyright file="GetEPAByEmployeeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.4.22">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get EPA by employee criteria.
    /// </summary>
    public class GetEPAByEmployeeCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public int? ProjectId { get; set; }

        public int? Status { get; set; }

        public override string GetSettingKey()
        {
            return "EPA_GetByEmployee";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
