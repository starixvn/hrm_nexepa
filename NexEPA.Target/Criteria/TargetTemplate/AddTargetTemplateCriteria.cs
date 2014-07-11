// --------------------------------------------------------------------------
// <copyright file="AddTargetTemplateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Target Template Criteria.
    /// </summary>
    public class AddTargetTemplateCriteria : CriteriaBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? TargetTypeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_Insert";
        }
    }
}
