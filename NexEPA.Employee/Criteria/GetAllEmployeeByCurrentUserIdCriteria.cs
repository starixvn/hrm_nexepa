// -----------------------------------------------------------------------
// <copyright file="GetAllEmployeeByCurrentUserIdCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.17">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get employees by current user Id.
    /// </summary>
    public class GetAllEmployeeByCurrentUserIdCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public string Keyword { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_GetMembers";
        }

        public override bool IsValid()
        {
             return ActorUserId > 0;
        }
    }
}
