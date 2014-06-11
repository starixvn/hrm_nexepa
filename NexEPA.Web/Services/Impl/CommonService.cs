// --------------------------------------------------------------------------
// <copyright file="CommonService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Web.Services.Impl
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Authority.Models;
    using NexEPA.Core.Enums;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;
    using NexEPA.Web.Repositories.Impl;
    using NexEPA.Web.Repositories.Interfaces;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// Common Service.
    /// </summary>
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;

        public CommonService(ICommonRepository commonRepository = null)
        {
            this._commonRepository = commonRepository ?? new CommonRepository();
        }

        /// <summary>
        /// Get user by domain username.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The User Session.</returns>
        public UserSession GetUserByDomainUsername(GetEmployeeByDomainUsernameCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                UserSession user = this._commonRepository.GetUserByDomainUsername(criteria);
                user.MenuGroups = GroupMenuItem(user.MenuItems);
                user.MenuItems = null;

                return user;
            }

            return null;
        }

        /// <summary>
        /// Get permission on function.
        /// </summary>
        /// <param name="functionKey">The function key.</param>
        /// <returns>The permission.</returns>
        public int GetPermission(string functionKey)
        {
            int policy = (int)Policy.None;

            if (HasPermissionOnFunction(functionKey, Policy.Host))
            {
                policy = (int)Policy.Host;
            }
            else if (HasPermissionOnFunction(functionKey, Policy.Read))
            {
                policy = (int)Policy.Read;
            }

            return policy;
        }

        /// <summary>
        /// Check current user has permission on current function or not.
        /// </summary>
        /// <param name="functionKey">The function key.</param>
        /// <param name="policy">THe policy.</param>
        /// <returns>True if has permission, false: otherwise.</returns>
        public bool HasPermissionOnFunction(string functionKey, Policy policy)
        {
            FunctionModel currentFunction = SessionManager.CurrentUser.Functions
                                    .Where(func => func.FunctionKey.Equals(functionKey)).FirstOrDefault();

            if (currentFunction != null)
            {
                switch (policy)
                {
                    case Policy.None:
                        return true;

                    case Policy.Read:
                        return currentFunction.IsRead || currentFunction.IsHost;

                    case Policy.Host:
                        return currentFunction.IsHost;
                }
            }

            return false;
        }

        /// <summary>
        /// Group Menu Item.
        /// </summary>
        /// <param name="menuItems">The list Menu item.</param>
        /// <returns>THe list Menu group.</returns>
        private IList<MenuGroup> GroupMenuItem(IList<MenuItem> menuItems)
        {
            var menuGroups = new List<MenuGroup>();
            IEnumerable<MenuItem> parentMenu = menuItems.Where(item => item.ParentId == null).ToList();

            foreach (MenuItem parent in parentMenu)
            {
                var menuGroupItem = new MenuGroup
                {
                    MenuId = parent.MenuId,
                    Name = parent.Name,
                    Url = parent.Url
                };

                menuGroupItem.MenuItems = menuItems.Where(menuItem => menuItem.ParentId == menuGroupItem.MenuId);
                menuGroups.Add(menuGroupItem);
            }

            return menuGroups;
        }
    }
}
