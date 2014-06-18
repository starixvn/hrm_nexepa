// --------------------------------------------------------------------------
// <copyright file="EPAFlowModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// EPA Flow Model.
    /// </summary>
    public class EPAFlowModel
    {
        public int GroupId { get; set; }

        public int DepartmentId { get; set; }

        public int EmployeeId { get; set; }

        public int ProcessId { get; set; }

        public List<int> EvaluatorId { get; set; }
    }
}
