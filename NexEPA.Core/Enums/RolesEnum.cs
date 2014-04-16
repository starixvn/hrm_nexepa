// --------------------------------------------------------------------------
// <copyright file="RolesEnum.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Core.Enums
{
    using NexEPA.Core.Extensions;

    public enum RolesEnum : short
    {
        /// <summary>
        /// System Admin item.
        /// </summary>
        [StringValue("SA")]
        SA = 1,

        /// <summary>
        /// Human Resource item.
        /// </summary>
        [StringValue("HR")]
        HR = 2,

        /// <summary>
        /// Project Manager item.
        /// </summary>
        [StringValue("PM")]
        PM = 3,

        /// <summary>
        /// Leader item.
        /// </summary>
        [StringValue("Leader")]
        Leader = 4,

        /// <summary>
        /// Member item.
        /// </summary>
        [StringValue("Member")]
        Member = 5,
    }
}
