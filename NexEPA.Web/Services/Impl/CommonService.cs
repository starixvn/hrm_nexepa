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
