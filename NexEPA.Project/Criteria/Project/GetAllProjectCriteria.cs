// --------------------------------------------------------------------------
// <copyright file="GetAllProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.20">Created</change>
// </history>

namespace NexEPA.Project.Criteria.Project
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Get All Project Criteria.
    /// </summary>
    public class GetAllProjectCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int? ProjectManager { get; set; }

        public int? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Project_Get";
        }
    }
}
