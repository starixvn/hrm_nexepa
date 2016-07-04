// --------------------------------------------------------------------------
// <copyright file="DeleteProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
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
    /// Delete Project Criteria.
    /// </summary>
    public class DeleteProjectCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public override bool IsValid()
        {
            return ProjectId > 0;
        }

        public override string GetSettingKey()
        {
            return "Project_Delete";
        }
    }
}
