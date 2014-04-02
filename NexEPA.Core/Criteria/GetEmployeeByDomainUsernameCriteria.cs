// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByDomainUsernameCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.28">Created</change>
// </history>

namespace NexEPA.Core.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// GetEmployeeByDomainUsername Criteria.
    /// </summary>
    public class GetEmployeeByDomainUsernameCriteria : CriteriaBase
    {
        public string DomainUsername { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetByDomainUsername";
        }
    }
}