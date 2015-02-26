// --------------------------------------------------------------------------
// <copyright file="GetRedmineTeamEffortCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// GetRedmineTeamEffortCriteria class.
    /// </summary>
    public class GetRedmineTeamEffortCriteria : CriteriaBase
    {
        public string NickName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(NickName) && ToDate >= FromDate;
        }

        public override string GetSettingKey()
        {
            return "ReportRedmine_TeamEffort";
        }
    }
}
