// --------------------------------------------------------------------------
// <copyright file="WebApiConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA
{
    using System.Web.Http;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// WebAPI Config.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register config.
        /// </summary>
        /// <param name="config">Http Configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Filters.Add(new ExceptionHandlingAttribute());
        }
    }
}
