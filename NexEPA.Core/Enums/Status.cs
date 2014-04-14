// --------------------------------------------------------------------------
// <copyright file="Status.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.11">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// ENUM Status.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// New items.
        /// </summary>
        [Description("New")]
        New = 0,

        /// <summary>
        /// In Progress item.
        /// </summary>
        [Description("In Progress")]
        InProgress = 1,

        /// <summary>
        /// Pending item.
        /// </summary>
        [Description("Pending")]
        Pending = 2,

        /// <summary>
        /// Resolved item.
        /// </summary>
        [Description("Resolved")]
        Resolved = 3,

        /// <summary>
        /// Closed item.
        /// </summary>
        [Description("Closed")]
        Closed = 4
    }
}
