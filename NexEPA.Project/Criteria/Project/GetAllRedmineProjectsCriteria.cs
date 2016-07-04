// --------------------------------------------------------------------------
// <copyright file="GetAllRedmineProjectsCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.11">Created</change>
// </history>

namespace NexEPA.Project.Criteria.Project
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// GetAllRedmineProjectsCriteria class.
    /// </summary>
    public class GetAllRedmineProjectsCriteria : CriteriaBase
    {
        public string ProjectName { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ProjectRedmine_GetAllProjects";
        }
    }
}
