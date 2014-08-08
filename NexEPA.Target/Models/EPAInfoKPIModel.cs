// -----------------------------------------------------------------------
// <copyright file="EPAInfoKPIModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.08.08">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using NexEPA.Employee.Models;

    /// <summary>
    /// The EPA Info model.
    /// </summary>
    public class EPAInfoKPIModel
    {
        public int TargetId { get; set; }

        public int KPIId { get; set; }

        public int KPIGroupId { get; set; }

        public IEnumerable<EmployeeModel> Employees { get; set; }
    }
}
