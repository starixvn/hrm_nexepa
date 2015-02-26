// --------------------------------------------------------------------------
// <copyright file="GetEmployeeByTeamProjectCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// GetEmployeeByTeamProjectCriteria class.
    /// </summary>
    public class GetEmployeeByTeamProjectCriteria : CriteriaBase
    {
        public int ActorUserId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int? GroupId { get; set; }

        public int? ProjectId { get; set; }

        public override bool IsValid()
        {
            return ActorUserId > 0 && ToDate >= FromDate;
        }

        public override string GetSettingKey()
        {
            return "Employee_GetByTeamProject";
        }
    }
}
