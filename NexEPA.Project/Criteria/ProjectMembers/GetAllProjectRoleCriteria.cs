// --------------------------------------------------------------------------
// <copyright file="GetAllProjectRoleCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Criteria.ProjectMembers
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get All Project Role Criteria.
    /// </summary>
    public class GetAllProjectRoleCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectRole_GetAll";
        }
    }
}
