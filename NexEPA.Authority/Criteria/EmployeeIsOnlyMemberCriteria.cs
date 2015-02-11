// --------------------------------------------------------------------------
// <copyright file="EmployeeIsOnlyMemberCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.11">Created</change>
// </history>

namespace NexEPA.Authority.Criteria
{
    using Fanex.Data.Repository;
    using NexEPA.Core.Extensions;

    /// <summary>
    /// EmployeeIsOnlyMemberCriteria class.
    /// </summary>
    public class EmployeeIsOnlyMemberCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        [Output]
        public bool IsOnlyMember { get; set; }

        public override string GetSettingKey()
        {
            return "Permission_IsOnlyMember";
        }

        public override bool IsValid()
        {
            return EmployeeId > 0;
        }
    }
}
