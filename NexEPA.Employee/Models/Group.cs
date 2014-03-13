// --------------------------------------------------------------------------
// <copyright file="Group.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    /// <summary>
    /// Employee group entity.
    /// </summary>
    public class Group
    {
        public int GroupId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int DepartmentId { get; set; }

        public int? LeaderId { get; set; }

        public bool IsDisabled { get; set; }
    }
}
