// --------------------------------------------------------------------------
// <copyright file="GetRedmineEmployeesByTeamProjectCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.13">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// GetEmployeeRedmineByTeamProjectCriteria class.
    /// </summary>
    public class GetRedmineEmployeesByTeamProjectCriteria : CriteriaBase
    {       
        public int? GroupId { get; set; }

        public string ProjectIds { get; set; }

        public string ExcludedGroupId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "EmployeeRedmine_GetByTeamProject";
        }
    }
}
