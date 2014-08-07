// --------------------------------------------------------------------------
// <copyright file="AddOrUpdateEPAScoreCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Add Or Update EPA Score Criteria.
    /// </summary>
    public class AddOrUpdateEPAScoreCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public int EvaluatorId { get; set; }

        public string Comment { get; set; }

        public string Score { get; set; }

        public string KPIs { get; set; }

        public int MarkedBy { get; set; }

        public DateTime MarkedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "EPA_InsertOrUpdateScore";
        }
    }
}
