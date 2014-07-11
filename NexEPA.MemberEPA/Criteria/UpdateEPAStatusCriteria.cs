// --------------------------------------------------------------------------
// <copyright file="UpdateEPAStatusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.13">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update EPA Status Criteria.
    /// </summary>
    public class UpdateEPAStatusCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public int Status { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return TargetId > 0 && EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "EPA_UpdateStatus";
        }
    }
}
