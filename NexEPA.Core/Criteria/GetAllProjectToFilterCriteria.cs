// -----------------------------------------------------------------------
// <copyright file="GetAllProjectToFilterCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.24">Created</change>
// </history>

namespace NexEPA.Core.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all project to filter criteria.
    /// </summary>
    public class GetAllProjectToFilterCriteria : CriteriaBase
    {
        public bool? IsShowClosed { get; set; }

        public int ActorUserId { get; set; }

        public int? IncludeProjectId { get; set; }

        public override string GetSettingKey()
        {
            return "Project_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
