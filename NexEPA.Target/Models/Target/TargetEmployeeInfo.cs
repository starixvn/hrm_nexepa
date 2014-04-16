// -----------------------------------------------------------------------
// <copyright file="TargetEmployeeInfo.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.16">Created</change>
// </history>

namespace NexEPA.Target.Models.Target
{
    using System;

    /// <summary>
    /// The model of Target's Employee.
    /// </summary>
    public class TargetEmployeeInfo
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string DomainUsername { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public int EmployeeLevelId { get; set; }

        public DateTime? JoinedDate { get; set; }

        public bool IsDisabled { get; set; }

        public int GroupId { get; set; }

        public string Name { get; set; }

        public int JobTitleId { get; set; }
    }
}
