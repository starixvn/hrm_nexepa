// --------------------------------------------------------------------------
// <copyright file="DepartmentModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NexEPA.Employee.Models;

    /// <summary>
    /// Department Model.
    /// </summary>
    public class DepartmentModel
    {
        public DepartmentModel()
        {
            Children = new List<DepartmentModel>();
        }

        public int DepartmentId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDisabled { get; set; }

        public int DepartmentLevel { get; set; }

        public int DisplayOrder { get; set; }

        public EmployeeModel Creator { get; set; }

        public EmployeeModel Manager { get; set; }

        public EmployeeModel Modifier { get; set; }

        public List<DepartmentModel> Children { get; set; }
    }
}
