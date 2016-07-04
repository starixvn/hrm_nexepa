// -----------------------------------------------------------------------
// <copyright file="UpdateGroupProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.17">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.Data.Repository;
    using NexEPA.Project.Models.GroupProject;

    /// <summary>
    /// Update group project criteria.
    /// </summary>
    public class UpdateGroupProjectCriteria : CriteriaBase
    {
        public int GroupProjectId { get; set; }

        public string Name { get; set; }

        public int ModifiedBy { get; set; }

        public string ProjectIds { get; set; }

        public override string GetSettingKey()
        {
            return "GroupProject_Update";
        }

        public override bool IsValid()
        {
            return GroupProjectId > 0;
        }
    }
}