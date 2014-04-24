// -----------------------------------------------------------------------
// <copyright file="Status.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.24">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// The ENUM Status.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Open items.
        /// </summary>
        [Description("Open")]
        Open = 0,

        /// <summary>
        /// Closed item.
        /// </summary>
        [Description("Closed")]
        Closed = 1,
    }
}
