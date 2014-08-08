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
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.Data.Repository;
    using NexEPA.Core.Repositories.Implement;
    using NexEPA.Core.Repositories.Interface;

    /// <summary>
    /// Query Service.
    /// </summary>
    public class BaseService
    {
        private IBaseRepository _repository;
        private IDynamicRepository _dynamicRepository;

        public BaseService(IDynamicRepository dynamicRepository = null, IBaseRepository repository = null)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
            _repository = repository ?? new BaseRepository();
        }

        public IBaseRepository Repository
        {
            get
            {
                return _repository;
            }
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
