// --------------------------------------------------------------------------
// <copyright file="InsertEmployeeCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.11">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Extensions;

    /// <summary>
    /// InsertEmployee Criteria.
    /// </summary>
    public class InsertEmployeeCriteria : CriteriaBase
    {
        [Output]
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public string DomainUsername { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public DateTime JoinedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Employee_Insert";
        }
    }
}
