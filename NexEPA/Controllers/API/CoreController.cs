// -----------------------------------------------------------------------
// <copyright file="CoreController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.27">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Model.Target;
    using NexEPA.Core.Services;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Core controller.
    /// </summary>
    public class CoreController : ApiController
    {
        private EPAService _epaService;

        public CoreController()
            : this(null)
        { 
        }

        public CoreController(EPAService epaService)
        {
            _epaService = epaService ?? new EPAService();
        }

        [HttpGet]
        public IEnumerable<TargetType> AllTargetType()
        {
            var criteria = new GetAllTargetTypeCriteria();
            return _epaService.GetAllTargetType(criteria);
        }
    }
}
