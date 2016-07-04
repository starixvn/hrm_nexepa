// --------------------------------------------------------------------------
// <copyright file="GetProjectByIdCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.24">Created</change>
// </history>

namespace NexEPA.Project.Criteria.Project
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Project By Id Criteria.
    /// </summary>
    public class GetProjectByIdCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public int ProjectId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Project_GetById";
        }
    }
}
