// -----------------------------------------------------------------------
// <copyright file="GetTargetToFilterCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.19">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.Target
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get target to filter.
    /// </summary>
    public class GetTargetToFilterCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int? Status { get; set; }

        public override string GetSettingKey()
        {
            return "Target_GetAll";
        }

        public override bool IsValid()
        {
            return ActorUserId > 0;
        }
    }
}
