// --------------------------------------------------------------------------
// <copyright file="RedmineResourcePlanningViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.ViewModels
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Report.Models.ResourcePlanningReport;

    /// <summary>
    /// RedmineResourcePlanningViewModel class.
    /// </summary>
    public class RedmineResourcePlanningViewModel
    {
        public IEnumerable<RedmineDetailResourcePlanningModel> DetailResourcePlannings { get; set; }

        public IEnumerable<RedmineProjectResourceModel> ProjectResources { get; set; }

        public IEnumerable<RedmineResourceDateStatusModel> ResourceDateStatuses { get; set; }

        public IEnumerable<RedmineMonthDateInfoModel> DateInfo { get; set; }

        public IEnumerable<RedmineYearMonthModel> MonthInfo { get; set; }
    }
}
