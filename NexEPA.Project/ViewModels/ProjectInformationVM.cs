// --------------------------------------------------------------------------
// <copyright file="ProjectInformationVM.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.18">Created</change>
// </history>

namespace NexEPA.Project.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.Project.Models;

    /// <summary>
    /// Project Information ViewModel.
    /// </summary>
    public class ProjectInformationVM
    {
        public ProjectModel ProjectModel { get; set; }

        public List<ProjectMilestoneModel> ProjectMilestone { get; set; }
    }
}
