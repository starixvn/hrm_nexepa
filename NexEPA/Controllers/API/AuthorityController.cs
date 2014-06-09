// -----------------------------------------------------------------------
// <copyright file="AuthorityController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Authority.Criteria;
    using NexEPA.Authority.Models;
    using NexEPA.Authority.Services.Implement;
    using NexEPA.Authority.Services.Interface;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// The Authority Controller.
    /// </summary>
    public class AuthorityController : BaseController
    {
        private IAuthorityService _authorityService;

        public AuthorityController()
            : this(null)
        {
        }

        public AuthorityController(IAuthorityService authorityService) 
        {
            _authorityService = authorityService ?? new AuthorityService();
        }

        [HttpGet]
        public IEnumerable<FunctionGroupModel> GetFunctionByRole(int roleId)
        {
            var criteria = new GetFunctionByRoleCriteria()
            {
                RoleId = roleId
            };

            return _authorityService.GetFunctionByRole(criteria);
        }

        [HttpGet]
        public IEnumerable<RoleSystemModel> GetAllSystemRole()
        {
            var criteria = new GetAllSystemRoleCriteria();

            return _authorityService.GetAllSystemRole(criteria);
        }
    }
}