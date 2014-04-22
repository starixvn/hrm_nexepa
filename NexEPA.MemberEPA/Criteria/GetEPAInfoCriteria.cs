// --------------------------------------------------------------------------
// <copyright file="GetEPAInfoCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get EPA Info Criteria.
    /// </summary>
    public class GetEPAInfoCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return TargetId > 0;
        }

        public override string GetSettingKey()
        {
            return "EPA_GetInfo";
        }
    }
}
