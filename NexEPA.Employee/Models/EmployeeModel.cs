// --------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="Nexcel Solutions Vietnam">
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
    using System.Collections.Generic;
    using NexEPA.Core.Models;

    /// <summary>
    /// The Employee model.
    /// </summary>
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string DomainUsername { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string Roles { get; set; }

        public int ProjectRoleId { get; set; }

        public int EmployeeLevelId { get; set; }

        public string Gravatar { get; set; }

        public bool IsExistsSetting { get; set; }

        public DateTime? JoinedDate { get; set; }

        public bool IsDisabled { get; set; }

        public Group Group { get; set; }

        public JobTitle JobTitle { get; set; }

        public List<CoreEPAModel> EPAInfo { get; set; }

        public string FinalScore { get; set; }
    }
}
