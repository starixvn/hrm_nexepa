// -----------------------------------------------------------------------
// <copyright file="TargetController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Templates.Target.Target
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Target.Services.Implement.Target;
    using NexEPA.Target.Services.Interface.Target;
    using NexEPA.Target.ViewModels.Target;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Target Controller.
    /// </summary>
    public class TargetController : BaseController
    {
        private ITargetServices _targetServices;

        public TargetController()
            : this(null)
        {
        }

        public TargetController(TargetServices targetServices)
        {
            _targetServices = targetServices ?? new TargetServices();
        }

        [HttpGet]
        public List<TargetItem> GetTarget()
        {
            return _targetServices.GetAllTarget();
        }
    }
}