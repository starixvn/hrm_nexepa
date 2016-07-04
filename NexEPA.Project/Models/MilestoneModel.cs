// --------------------------------------------------------------------------
// <copyright file="MilestoneModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Milestone Model.
    /// </summary>
    public class MilestoneModel
    {
        public int MilestoneId { get; set; }

        [Required(ErrorMessage = "The field Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        [Required(ErrorMessage = "The field Start Date is required.")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "The field End Date is required.")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "The field Status is required.")]
        public byte? Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
