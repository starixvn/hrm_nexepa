// --------------------------------------------------------------------------
// <copyright file="UpdateProjectStatusCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.7">Created</change>
// </history>

namespace NexEPA.Project.Criteria.Project
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Update Project Status Criteria.
    /// </summary>
    public class UpdateProjectStatusCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public int Status { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public override bool IsValid()
        {
            return ProjectId > 0 && Status >= 0;
        }

        public override string GetSettingKey()
        {
            return "Project_UpdateStatus";
        }
    }
}
