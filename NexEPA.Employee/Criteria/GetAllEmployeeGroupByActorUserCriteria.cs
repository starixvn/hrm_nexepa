// --------------------------------------------------------------------------
// <copyright file="GetAllEmployeeGroupByActorUserCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.11">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all employee group criteria.
    /// </summary>
    public class GetAllEmployeeGroupByActorUserCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public string ExcludedGroupId { get; set; }

        public override string GetSettingKey()
        {
            return "EmployeeGroup_GetByActorUser";
        }

        public override bool IsValid()
        {
            return ActorUserId > 0;
        }
    }
}
