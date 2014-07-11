// -----------------------------------------------------------------------
// <copyright file="GetTargetByIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.07">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.Target
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Target by ID criteria.
    /// </summary>
    public class GetTargetByIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int ActorUserId { get; set; }

        public override string GetSettingKey()
        {
            return "Target_GetById";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
