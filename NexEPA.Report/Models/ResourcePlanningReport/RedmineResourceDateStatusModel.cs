// --------------------------------------------------------------------------
// <copyright file="RedmineResourceDateStatusModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Models.ResourcePlanningReport
{
    using NexEPA.Employee.Models;

    /// <summary>
    /// RedmineResourceDateStatusModel class.
    /// </summary>
    public class RedmineResourceDateStatusModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineDateStatusModel DateStatus { get; set; }
    }
}
