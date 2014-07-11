// -----------------------------------------------------------------------
// <copyright file="GetTargetCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.Target
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Target criteria.
    /// </summary>
    public class GetTargetCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int? TargetTypeId { get; set; }

        public int? ProjectId { get; set; }

        public int? ProjectManager { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? Status { get; set; }

        public override string GetSettingKey()
        {
            return "Target_Get";
        }

        public override bool IsValid()
        {
            if (StartDate != null && EndDate != null)
            {
                return StartDate <= EndDate;
            }

            return true;
        }
    }
}
