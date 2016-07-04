// -----------------------------------------------------------------------
// <copyright file="GroupProjectModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.15">Created</change>
// </history>

namespace NexEPA.Project.Models.GroupProject
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Group project model.
    /// </summary>
    public class GroupProjectModel
    {
        public GroupProjectModel()
        {
            Projects = new List<GroupProjectDetailModel>();
        }

        public int GroupProjectId { get; set; }

        public string Name { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public IList<GroupProjectDetailModel> Projects { get; set; }
    }
}