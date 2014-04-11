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
    using Fanex.Common.Extensions;

    /// <summary>
    /// ENUM Status.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// New items.
        /// </summary>
        [StringValue("New")]
        New = 0,

        /// <summary>
        /// In Progress item.
        /// </summary>
        [StringValue("In Progress")]
        InProgress = 1,

        /// <summary>
        /// Pending item.
        /// </summary>
        [StringValue("Pending")]
        Pending = 2,

        /// <summary>
        /// Resolved item.
        /// </summary>
        [StringValue("Resolved")]
        Resolved = 3,

        /// <summary>
        /// Closed item.
        /// </summary>
        [StringValue("Closed")]
        Closed = 4
    }
}
