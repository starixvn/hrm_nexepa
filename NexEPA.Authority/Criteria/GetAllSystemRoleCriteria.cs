// -----------------------------------------------------------------------
// <copyright file="GetAllSystemRoleCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.05">Created</change>
// </history>

namespace NexEPA.Authority.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all system role criteria.
    /// </summary>
    public class GetAllSystemRoleCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "SystemRole_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
