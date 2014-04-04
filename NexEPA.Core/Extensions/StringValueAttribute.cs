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

    /// <summary>
    /// StringValue Attribute.
    /// </summary>
    public class StringValueAttribute : Attribute
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
