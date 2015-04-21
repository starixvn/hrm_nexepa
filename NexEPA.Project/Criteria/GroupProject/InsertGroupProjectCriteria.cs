// -----------------------------------------------------------------------
// <copyright file="InsertGroupProjectCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.16">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.Data.Repository;
    using NexEPA.Core.Extensions;
    using NexEPA.Project.Models.GroupProject;

    /// <summary>
    /// Insert group project criteria.
    /// </summary>
    public class InsertGroupProjectCriteria : CriteriaBase
    {
        [Output]
        public int GroupProjectId { get; set; }

        public string Name { get; set; }

        public int CreatedBy { get; set; }

        public string ProjectIds { get; set; }

        public override string GetSettingKey()
        {
            return "GroupProject_Insert";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}