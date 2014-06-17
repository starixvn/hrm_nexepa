// -----------------------------------------------------------------------
// <copyright file="GetAppraisalFormCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.17">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get appraisal form criteria.
    /// </summary>
    public class GetAppraisalFormCriteria : CriteriaBase
    {
        public int EPATypeId { get; set; }

        public int EmployeeId { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public override string GetSettingKey()
        {
            return string.Empty;
        }

        public override bool IsValid()
        {
            return EPATypeId > 0 && EmployeeId > 0;
        }
    }
}
