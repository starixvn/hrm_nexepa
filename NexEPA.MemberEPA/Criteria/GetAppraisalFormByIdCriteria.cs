// -----------------------------------------------------------------------
// <copyright file="GetAppraisalFormByIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.20">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Appraisal Form by Id criteria.
    /// </summary>
    public class GetAppraisalFormByIdCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_GetById";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
