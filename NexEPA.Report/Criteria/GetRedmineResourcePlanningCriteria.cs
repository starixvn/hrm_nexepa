// --------------------------------------------------------------------------
// <copyright file="GetRedmineResourcePlanningCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// GetRedmineResourcePlanningCriteria class.
    /// </summary>
    public class GetRedmineResourcePlanningCriteria : CriteriaBase
    {
        public string NickName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int? ProjectId { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(NickName) && ToDate >= FromDate;
        }

        public override string GetSettingKey()
        {
            return "ReportRedmine_ResourcePlanning";
        }
    }
}
