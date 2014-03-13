// --------------------------------------------------------------------------
// <copyright file="GetGroupByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// GetGroupById Criteria.
    /// </summary>
    public class GetGroupByIdCriteria : CriteriaBase
    {
        public int GroupId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "EmployeeGroup_GetById";
        }
    }
}
