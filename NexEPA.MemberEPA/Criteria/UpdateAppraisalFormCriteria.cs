// -----------------------------------------------------------------------
// <copyright file="UpdateAppraisalFormCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.20">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update Appraisal Form criteria.
    /// </summary>
    public class UpdateAppraisalFormCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public string ProcessDetails { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Update";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
