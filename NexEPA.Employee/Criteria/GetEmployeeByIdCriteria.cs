// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Extensions;

    /// <summary>
    /// GetEmployeeById Criteria.
    /// </summary>
    public class GetEmployeeByIdCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetById";
        }
    }
}
