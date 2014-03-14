// --------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
// </history>

namespace NexEPA.Core.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Repositories;

    /// <summary>
    /// Query Service.
    /// </summary>
    public class BaseService
    {
        protected readonly BaseRepository _Repository;
        private readonly IDynamicRepository _dynamicRepository;

        public BaseService(IDynamicRepository dynamicRepository = null, BaseRepository repository = null)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
            _Repository = repository ?? new BaseRepository();
        }

        /// <summary>
        /// Function to get list object from database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="criteria">Criteria to query database.</param>
        /// <returns>List Object.</returns>
        public virtual IEnumerable<T> GetData<T>(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                return _dynamicRepository.Fetch<T>(criteria);
            }

            return new List<T>();
        }

        /// <summary>
        /// Function to get object from database.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="criteria">Criteria to query database.</param>
        /// <returns>The object.</returns>
        public virtual T GetDataEntity<T>(ICriteria criteria)
        {
            if (criteria.IsValid())
            {
                return _dynamicRepository.Get<T>(criteria);
            }

            return default(T);
        }
    }
}
