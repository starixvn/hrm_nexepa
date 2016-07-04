// --------------------------------------------------------------------------
// <copyright file="MemberModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Member Models.
    /// </summary>
    public class MemberModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "The field Employee is required.")]
        public int? EmployeeId { get; set; }

        public string ProjectRoleIds { get; set; }

        public int ProjectRoleId { get; set; }

        public DateTime? JoinDate { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
