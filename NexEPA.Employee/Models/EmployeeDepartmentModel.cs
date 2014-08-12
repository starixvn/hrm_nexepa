// -----------------------------------------------------------------------
// <copyright file="EmployeeDepartmentModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.08.12">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The employee information in department model.
    /// </summary>
    public class EmployeeDepartmentModel
    {
        public IEnumerable<EmployeeModel> PersonalFlowSettingEmployees { get; set; }

        public IEnumerable<EmployeeModel> DefaultFlowSettingEmployees { get; set; }
    }
}
