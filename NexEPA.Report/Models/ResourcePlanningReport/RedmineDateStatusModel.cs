// --------------------------------------------------------------------------
// <copyright file="RedmineDateStatusModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Models.ResourcePlanningReport
{
    using System;

    /// <summary>
    /// RedmineDateStatusModel class.
    /// </summary>
    public class RedmineDateStatusModel
    {
        public string DayName { get; set; }

        public DateTime WorkDate { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsOffDay { get; set; }

        public bool IsLeaveDay { get; set; }

        public int LeaveTypeId { get; set; }
    }
}