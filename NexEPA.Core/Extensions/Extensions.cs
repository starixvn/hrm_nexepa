// --------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.11">Created</change>
// </history>

namespace NexEPA.Core.Extensions
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Extensions Class.
    /// </summary>
    public class Extensions
    {
        public static string GetPropertyName<T>(Expression<Func<T>> exp)
        {
            return ((MemberExpression)exp.Body).Member.Name;
        }
    }
}
