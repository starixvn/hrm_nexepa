// --------------------------------------------------------------------------
// <copyright file="ProjectManagerModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.21">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    /// <summary>
    /// Project Manager Model.
    /// </summary>
    public class ProjectManagerModel
    {
        public int ProjectManagerId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }
    }
}
