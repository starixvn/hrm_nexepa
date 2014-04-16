// -----------------------------------------------------------------------
// <copyright file="AddTargetEmployeeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.16">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Add Target Employee criteria.
    /// </summary>
    public class AddTargetEmployeeCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_Add";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
