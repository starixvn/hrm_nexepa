// -----------------------------------------------------------------------
// <copyright file="GetFunctionByRoleCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Authority.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get function by role criteria.
    /// </summary>
    public class GetFunctionByRoleCriteria : CriteriaBase
    {
        public int RoleId { get; set; }

        public override string GetSettingKey()
        {
            return "Function_GetByRole";
        }

        public override bool IsValid()
        {
            return RoleId > 0;
        }
    }
}
