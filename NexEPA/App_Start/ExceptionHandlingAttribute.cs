// --------------------------------------------------------------------------
// <copyright file="ExceptionHandlingAttribute.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.14">Created</change>
// </history>

namespace NexEPA
{
    using System;
    using System.Web;
    using System.Web.Http.Filters;
    using Fanex.Logging;

    /// <summary>
    /// Exception Handling Attribute.
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Function to write log for API controller exception.
        /// </summary>
        /// <param name="context">HttpActionExecuted Context.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            var localFlag = context.Request.Properties["MS_IsLocal"] as Lazy<bool>;
            if (localFlag == null || !localFlag.Value)
            {
                var exception = context.Exception;
                string message = exception.Message +
                        "\r\nSource: " + exception.Source +
                        "\r\nDomain: " + HttpContext.Current.Request.Headers["Host"] +
                        "\r\nTargetSite: " + exception.TargetSite +
                        "\r\nStackTrace: " + exception.StackTrace;

                LogService.Error(message);
            }
        }
    }
}