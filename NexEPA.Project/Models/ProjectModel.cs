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
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Project Model.
    /// </summary>
    public class ProjectModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
     
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
     
        public int ProjectManager { get; set; }

        public int Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
