// --------------------------------------------------------------------------
// <copyright file="OutputAttribute.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.13">Created</change>
// </history>

namespace NexEPA.Core.Extensions
{
    using System;

    /// <summary>
    /// Output Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class OutputAttribute : Attribute
    {
        public OutputAttribute()
        {
        }
    }
}
