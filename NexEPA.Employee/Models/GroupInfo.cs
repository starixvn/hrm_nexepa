// -----------------------------------------------------------------------
// <copyright file="GroupInfo.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    /// <summary>
    /// The Group Info model.
    /// </summary>
    public class GroupInfo
    {
        public int GroupId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }
    }
}
