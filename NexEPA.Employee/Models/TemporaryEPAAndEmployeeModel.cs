// -----------------------------------------------------------------------
// <copyright file="TemporaryEPAAndEmployeeModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.07.07">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The temporary model contain employees and EPA information.
    /// </summary>
    public class TemporaryEPAAndEmployeeModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }

        public IEnumerable<TemporaryEPAModel> RawEPAs { get; set; }
    }
}
