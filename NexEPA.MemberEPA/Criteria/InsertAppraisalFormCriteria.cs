// -----------------------------------------------------------------------
// <copyright file="InsertAppraisalFormCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.18">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Insert Appraisal Form criteria.
    /// </summary>
    public class InsertAppraisalFormCriteria : CriteriaBase
    {
        public int EPATypeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int EmployeeId { get; set; }

        public string ProcessDetails { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public override string GetSettingKey()
        {
            return "EPAForm_Insert";
        }

        public override bool IsValid()
        {
            return EPATypeId > 0 && EmployeeId > 0;
        }
    }
}
