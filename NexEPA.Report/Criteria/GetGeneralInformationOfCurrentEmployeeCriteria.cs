// -----------------------------------------------------------------------
// <copyright file="GetGeneralInformationOfCurrentEmployeeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get general information of current employee that used in dashboard.
    /// </summary>
    public class GetGeneralInformationOfCurrentEmployeeCriteria : CriteriaBase
    {
        public int EmployeeId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Report_MySpace";
        }
    }
}
