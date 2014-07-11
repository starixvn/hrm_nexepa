// -----------------------------------------------------------------------
// <copyright file="GetAllProcessPeriodCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.19">Created</change>
// </history>

namespace NexEPA.MemberEPA.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all process period criteria.
    /// </summary>
    public class GetAllProcessPeriodCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "ProcessPeriod_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
