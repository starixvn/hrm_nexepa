// -----------------------------------------------------------------------
// <copyright file="GetEPATypeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.14">Created</change>
// </history>

namespace NexEPA.Target.Criteria.TargetEmployeeSetting
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get EPA type criteria.
    /// </summary>
    public class GetEPATypeCriteria : CriteriaBase
    {
        public int? EPATypeId { get; set; }

        public override string GetSettingKey()
        {
            return "EPAType_Get";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
