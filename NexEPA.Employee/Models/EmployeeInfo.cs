// --------------------------------------------------------------------------
// <copyright file="EmployeeInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
//   <change who="Josh.Phan" date="2014.04.03">Changed</change>
// </history>

namespace NexEPA.Employee.Models
{
    using System;

    /// <summary>
    /// Employee class.
    /// </summary>
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string DomainUsername { get; set; }

        public string FullName { get; set; } 

        public string NickName { get; set; } 

        public string Email { get; set; }

        public string Skype { get; set; }

        public int GroupId { get; set; }

        public int EmployeeLevelId { get; set; }

        public int JobTitleId { get; set; }

        public int LeaderId { get; set; }

        public DateTime? JoinedDate { get; set; }

        public bool IsDisabled { get; set; }
    }
}
