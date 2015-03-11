// --------------------------------------------------------------------------
// <copyright file="GetAllRedmineProjectsCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
