// -----------------------------------------------------------------------
// <copyright file="GetUserPermissionOnTargetCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.07.09">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get user permission on target criteria.
    /// </summary>
    public class GetUserPermissionOnTargetCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "EPADetail_CheckPermission";
        }

        public override bool IsValid()
        {
            return ActorUserId > 0 && TargetId > 0;
        }
    }
}
