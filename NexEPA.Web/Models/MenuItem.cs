// -----------------------------------------------------------------------
// <copyright file="MenuItem.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.11">Created</change>
// </history>

namespace NexEPA.Web.Models
{
    /// <summary>
    /// The Menu Item.
    /// </summary>
    public class MenuItem
    {
        public int MenuId { get; set; }

        public string Name { get; set; }

        public string MenuLink { get; set; }

        public int? ParentId { get; set; }

        public int FunctionId { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsDisabled { get; set; }
    }
}
