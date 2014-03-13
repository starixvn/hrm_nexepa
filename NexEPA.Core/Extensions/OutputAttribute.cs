// --------------------------------------------------------------------------
// <copyright file="OutputAttribute.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
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
    public class OutputAttribute : Attribute
    {
        private bool _isOutput;

        public OutputAttribute()
        {
            this._isOutput = true;
        }

        public virtual bool IsOutput
        {
            get { return _isOutput; }
        }
    }
}
