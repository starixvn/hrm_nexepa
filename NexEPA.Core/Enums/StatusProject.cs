// --------------------------------------------------------------------------
// <copyright file="StatusProject.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.19">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// ENUM Status for Project.
    /// </summary>
    public enum StatusProject
    {
        /// <summary>
        /// New items.
        /// </summary>
        [Description("New")]
        New = 0,

        /// <summary>
        /// In Progress item.
        /// </summary>
        [Description("Open")]
        Open = 1,

        /// <summary>
        /// Pending item.
        /// </summary>
        [Description("Closed")]
        Close = 2,
    }
}
