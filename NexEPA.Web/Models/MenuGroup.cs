// -----------------------------------------------------------------------
// <copyright file="MenuGroup.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.11">Created</change>
// </history>

namespace NexEPA.Web.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The Menu Group.
    /// </summary>
    public class MenuGroup : MenuItem
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}
