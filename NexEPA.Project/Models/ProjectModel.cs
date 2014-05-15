// --------------------------------------------------------------------------
// <copyright file="ProjectModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.18">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Project Model.
    /// </summary>
    public class ProjectModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "The field Project Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The field Start Date is required.")]  
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "The field Project Manager is required.")]
        public int? ProjectManager { get; set; }

        [Required(ErrorMessage = "The field Status is required.")]
        public byte? Status { get; set; }

        public string StatusName { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int? Members { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public int Permission { get; set; }

        public MilestoneModel Milestone { get; set; }
    }
}
