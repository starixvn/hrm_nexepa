// -----------------------------------------------------------------------
// <copyright file="DeleteTargetEmployeeCriteria.cs" company="Nexcel Solutions Viet Nam">
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
    /// Delete Target Employee criteria.
    /// </summary>
    public class DeleteTargetEmployeeCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_Delete";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
