// --------------------------------------------------------------------------
// <copyright file="StringValueAttribute.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Core.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// StringValue Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class StringValueAttribute : Attribute
    {
        private readonly string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
