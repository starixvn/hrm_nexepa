// --------------------------------------------------------------------------
// <copyright file="EPAProjectRoleModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    /// <summary>
    /// EPA Project Role Model.
    /// </summary>
    public class EPAProjectRoleModel
    {
        public int ProjectRoleId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
