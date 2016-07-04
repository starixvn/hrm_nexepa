// --------------------------------------------------------------------------
// <copyright file="GetRedminePersonalEffortCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Criteria
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// GetRedminePersonalEffortCriteria class.
    /// </summary>
    public class GetRedminePersonalEffortCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool IsFilterGroup { get; set; }

        public string ProjectIds { get; set; }

        public int? GroupProjectId { get; set; }

        public override bool IsValid()
        {
            return EmployeeId > 0 && ToDate >= FromDate;
        }

        public override string GetSettingKey()
        {
            return "ReportRedmine_PersonalEffort";
        }
    }
}
