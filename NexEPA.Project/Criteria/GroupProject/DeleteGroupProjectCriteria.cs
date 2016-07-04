// -----------------------------------------------------------------------
// <copyright file="DeleteGroupProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.17">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete group project criteria.
    /// </summary>
    public class DeleteGroupProjectCriteria : CriteriaBase
    {
        public int GroupProjectId { get; set; }

        public int ModifiedBy { get; set; }

        public override string GetSettingKey()
        {
            return "GroupProject_Delete";
        }

        public override bool IsValid()
        {
            return GroupProjectId > 0;
        }
    }
}