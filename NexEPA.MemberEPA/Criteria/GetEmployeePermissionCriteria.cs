// --------------------------------------------------------------------------
// <copyright file="GetEmployeePermissionCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.15">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Employee Permission Criteria.
    /// </summary>
    public class GetEmployeePermissionCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return ActorUserId > 0 && TargetId > 0 && EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "EPA_CheckPermission";
        }
    }
}
