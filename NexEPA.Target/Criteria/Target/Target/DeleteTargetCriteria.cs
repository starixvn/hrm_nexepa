// -----------------------------------------------------------------------
// <copyright file="DeleteTargetCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.Target
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Delete Target criteria.
    /// </summary>
    public class DeleteTargetCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "Target_Delete";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
