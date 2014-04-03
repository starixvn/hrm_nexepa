// -----------------------------------------------------------------------
// <copyright file="GetAllEmployeeCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get all employee criteria.
    /// </summary>
    public class GetAllEmployeeCriteria : CriteriaBase
    {
        public bool? IsDisabled { get; set; }

        [SpParam(Ignored = true)]
        public string SearchKeyword { get; set; }

        [SpParam(Ignored = true)]
        public int? GroupId { get; set; }

        public override string GetSettingKey()
        {
            return "Employee_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
