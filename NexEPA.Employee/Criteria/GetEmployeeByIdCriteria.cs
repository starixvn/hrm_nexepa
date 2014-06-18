// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.17">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get Employee By Id.
    /// </summary>
    public class GetEmployeeByIdCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return EmployeeId > 0;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetById";
        }
    }
}
