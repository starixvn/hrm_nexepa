// --------------------------------------------------------------------------
// <copyright file="ResourceController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Web.Http;
    using NexEPA.Resources;

    /// <summary>
    /// Resource Controller.
    /// </summary>
    public class ResourceController : ApiController
    {
        /// <summary>
        /// Function to get resource by language.
        /// </summary>
        /// <param name="lang">Language input.</param>
        /// <returns>String resource JSON format.</returns>
        public string Get(string lang)
        {
            ResourceManager resourceManager = null;
            switch (lang)
            {
                case "en":
                    resourceManager = new ResourceManager(typeof(Lang)) { IgnoreCase = true };
                    break;
                case "de":
                    resourceManager = new ResourceManager(typeof(Lang_de)) { IgnoreCase = true };
                    break;
                default:
                    resourceManager = new ResourceManager(typeof(Lang)) { IgnoreCase = true };
                    break;
            }

            ResourceSet resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            var dictionary = resourceSet.Cast<DictionaryEntry>();
            var items = dictionary.Select(dict => string.Format("\"{0}\" : \"{1}\"", dict.Key, string.Join(",", dict.Value)));
            var jsonString = "{" + string.Join(",", items) + "}";

            return jsonString;
        }
    }
}
