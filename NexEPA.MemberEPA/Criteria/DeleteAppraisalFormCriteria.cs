// -----------------------------------------------------------------------
// <copyright file="DeleteAppraisalFormCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.23">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete Appraisal Form criteria.
    /// </summary>
    public class DeleteAppraisalFormCriteria : CriteriaBase
    {
        public int EPAId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Delete";
        }

        public override bool IsValid()
        {
            return EPAId > 0;
        }
    }
}
