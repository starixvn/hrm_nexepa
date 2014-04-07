// --------------------------------------------------------------------------
// <copyright file="DeleteTargetTemplateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Delete Target Template Criteria.
    /// </summary>
    public class DeleteTargetTemplateCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_Delete";
        }
    }
}
