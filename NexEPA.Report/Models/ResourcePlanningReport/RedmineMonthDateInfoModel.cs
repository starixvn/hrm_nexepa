// --------------------------------------------------------------------------
// <copyright file="RedmineMonthDateInfoModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Models.ResourcePlanningReport
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;

    /// <summary>
    /// RedmineMonthDateInfoModel class.
    /// </summary>
    public class RedmineMonthDateInfoModel
    {
        public RedmineYearMonthModel Month { get; set; }

        public RedmineDateStatusModel DateStatus { get; set; }
    }
}