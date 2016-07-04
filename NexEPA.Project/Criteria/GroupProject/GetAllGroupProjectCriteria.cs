// -----------------------------------------------------------------------
// <copyright file="GetAllGroupProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.15">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get all group project criteria.
    /// </summary>
    public class GetAllGroupProjectCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "GroupProject_Get";
        }
    }
}