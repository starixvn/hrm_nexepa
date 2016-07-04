// --------------------------------------------------------------------------
// <copyright file="EPAFlowModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Models
{
    using NexEPA.Employee.Models;

    /// <summary>
    /// EPA Flow Model.
    /// </summary>
    public class EPAFlowModel
    {
        public int ProcessId { get; set; }

        public EmployeeModel EvaluatorInfo { get; set; }
    }
}
