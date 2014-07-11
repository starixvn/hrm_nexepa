// --------------------------------------------------------------------------
// <copyright file="GetAllTargetTemplateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetTemplate
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get All Target Template Criteria.
    /// </summary>
    public class GetAllTargetTemplateCriteria : CriteriaBase
    {
        public int? TargetTypeId { get; set; }

        public bool? IsDisabled { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "TargetTemplate_GetAll";
        }
    }
}
