// -----------------------------------------------------------------------
// <copyright file="GetAllProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.16">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all project criteria.
    /// </summary>
    public class GetAllProjectCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "GroupProject_GetAllProject";
        }
    }
}