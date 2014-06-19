// --------------------------------------------------------------------------
// <copyright file="ProcessTypeEnum.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.19">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Process Type ENUM.
    /// </summary>
    public enum ProcessTypeEnum : int
    {
        /// <summary>
        /// Process by PM.
        /// </summary>
        [Description("Process by PM")]
        PM = 1,

        /// <summary>
        /// The Admin.
        /// </summary>
        [Description("Admin")]
        Admin = 2,

        /// <summary>
        /// The Employee.
        /// </summary>
        [Description("Self")]
        Self = 3,

        /// <summary>
        /// The Evaluator.
        /// </summary>
        [Description("Evaluator")]
        Evaluator = 4,

        /// <summary>
        /// The Reviewer.
        /// </summary>
        [Description("Reviewer")]
        Reviewer = 5,
    }
}
