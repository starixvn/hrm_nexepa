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
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Core.Utilities;

    /// <summary>
    /// Core controller.
    /// </summary>
    public class CoreController : ApiController
    {
        private IEPAService _epaService;

        public CoreController()
            : this(null)
        { 
        }

        public CoreController(IEPAService epaService)
        {
            _epaService = epaService ?? new EPAService();
        }

        [HttpGet]
        public IEnumerable<TargetType> AllTargetType()
        {
            var criteria = new GetAllTargetTypeCriteria();
            return _epaService.GetAllTargetType(criteria);
        }

        [HttpGet]
        public IEnumerable<dynamic> GetAllProject()
        {
            var criteria = new GetAllProjectToFilterCriteria();
            return _epaService.GetAllProjectToFilter(criteria);
        }

        [HttpGet]
        public IEnumerable<SelectListModel> GetAllStatus()
        {
            return EnumStatus.GetAllStatus();
        }
    }
}
