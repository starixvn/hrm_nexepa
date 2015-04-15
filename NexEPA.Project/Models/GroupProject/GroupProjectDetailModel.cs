// -----------------------------------------------------------------------
// <copyright file="GroupProjectDetailModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.14">Created</change>
// </history>

namespace NexEPA.Project.Models.GroupProject
{
    /// <summary>
    /// Group project detail model.
    /// </summary>
    public class GroupProjectDetailModel
    {
        public int GroupProjectId { get; set; }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public int DatabaseId { get; set; }
    }
}