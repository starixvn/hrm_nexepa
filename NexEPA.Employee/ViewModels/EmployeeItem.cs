// -----------------------------------------------------------------------
// <copyright file="EmployeeItem.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.ViewModels
{
    using System;

    /// <summary>
    /// The View Model of Employee.
    /// </summary>
    public class EmployeeItem
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string JoinedDate { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int JobTitleId { get; set; }

        public string JobTitleName { get; set; }
    }
}
