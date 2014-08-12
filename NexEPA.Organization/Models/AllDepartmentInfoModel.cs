// -----------------------------------------------------------------------
// <copyright file="AllDepartmentInfoModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.08.12">Created</change>
// </history>

namespace NexEPA.Organization.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// All department information model.
    /// </summary>
    public class AllDepartmentInfoModel
    {
        public DepartmentModel TreeDepartment { get; set; }

        public List<DepartmentModel> AllDepartments { get; set; }
    }
}
