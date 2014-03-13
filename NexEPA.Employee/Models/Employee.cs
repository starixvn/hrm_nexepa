// --------------------------------------------------------------------------
// <copyright file="Employee.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    using System;

    /// <summary>
    /// Employee class.
    /// </summary>
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public string DomainUsername { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public DateTime JoinedDate { get; set; }
    }
}
