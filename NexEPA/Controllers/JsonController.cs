// --------------------------------------------------------------------------
// <copyright file="JsonController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// JSON Controller.
    /// </summary>
    public class JsonController : Controller
    {
        /// <summary>
        /// Function to override ActionResult JSON.
        /// </summary>
        /// <param name="data">Data input.</param>
        /// <param name="behavior">Behavior of request.</param>
        /// <returns>Action Result.</returns>
        protected new ActionResult Json(object data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            if (Request.RequestType == WebRequestMethods.Http.Get && behavior == JsonRequestBehavior.DenyGet)
            {
                throw new InvalidOperationException("GET is no permitted for this request");
            }

            var jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(data, jsonSerializerSettings),
                ContentType = "application/json"
            };
            return jsonResult;
        }
    }
}
