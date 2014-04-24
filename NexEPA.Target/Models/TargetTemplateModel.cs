// --------------------------------------------------------------------------
// <copyright file="TargetTemplateModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Target Template Model.
    /// </summary>
    public class TargetTemplateModel
    {
        public int TargetId { get; set; }

        [Required(ErrorMessage = "The field Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The field Target Type is required.")]
        public byte? TargetTypeId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public int KPIs { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public string KPIIds { get; set; }
    }
}
