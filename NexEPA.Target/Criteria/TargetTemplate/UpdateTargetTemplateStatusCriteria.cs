// -----------------------------------------------------------------------
// <copyright file="UpdateTargetTemplateStatusCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.29">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update Target Template Status criteria.
    /// </summary>
    public class UpdateTargetTemplateStatusCriteria : CriteriaBase
    {
        public int TargetTemplateId { get; set; }

        public bool IsDisabled { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetTemplate_UpdateStatus";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
