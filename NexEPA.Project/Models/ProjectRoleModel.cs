// --------------------------------------------------------------------------
// <copyright file="ProjectRoleModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    /// <summary>
    /// Project Role Model.
    /// </summary>
    public class ProjectRoleModel
    {
        public int ProjectRoleId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public bool Selected { get; set; }
    }
}
