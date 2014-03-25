// --------------------------------------------------------------------------
// <copyright file="ProjectStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.19">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using Fanex.Common.Extensions;

    /// <summary>
    /// ENUM ProjectStatus.
    /// </summary>
    public enum ProjectStatus
    {
         /// <summary>
        /// CheckOut item.
        /// </summary>
        [StringValue("Active")]
        Active = 1,

        /// <summary>
        /// CheckIn item.
        /// </summary>
        [StringValue("Closed")]
        Closed = 2
    }
}
