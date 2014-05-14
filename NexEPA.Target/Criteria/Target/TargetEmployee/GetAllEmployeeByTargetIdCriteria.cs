// -----------------------------------------------------------------------
// <copyright file="GetAllEmployeeByTargetIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.16">Created</change>
// </history>

namespace NexEPA.Target.Criteria.Target.TargetEmployee
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get all Employee by Target Id criteria.
    /// </summary>
    public class GetAllEmployeeByTargetIdCriteria : CriteriaBase
    {
        public int TargetId { get; set; }

        public override string GetSettingKey()
        {
            return "TargetEmployee_GetAll";
        }

        public override bool IsValid()
        {
            return TargetId > 0;
        }
    }
}
