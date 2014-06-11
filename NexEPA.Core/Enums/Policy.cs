// -----------------------------------------------------------------------
// <copyright file="Policy.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.10">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// The ENUM Policy.
    /// </summary>
    public enum Policy
    {
        /// <summary>
        /// None policy.
        /// </summary>
        [Description("None")]
        None = 0,

        /// <summary>
        /// Read policy.
        /// </summary>
        [Description("Read")]
        Read = 1,

        /// <summary>
        /// Host policy.
        /// </summary>
        [Description("Host")]
        Host = 2,
    }
}
