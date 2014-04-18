// --------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA.Controllers
{
    using System.Web.Mvc;
    using NexEPA.Web.Models;

    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.User = SessionManager.CurrentUser.FullName + " - " + SessionManager.CurrentUser.NickName;
            return View();
        }
    }
}
