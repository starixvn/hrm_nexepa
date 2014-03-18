// --------------------------------------------------------------------------
// <copyright file="EmployeeService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
// </history>

namespace NexEPA.Employee.Services
{
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Employee.Criteria;

    /// <summary>
    /// Employee service.
    /// </summary>
    public class EmployeeService : BaseService
    {
        /// <summary>
        /// Function to insert new employee.
        /// </summary>
        /// <param name="criteria">InsertEmployee Criteria.</param>
        /// <returns>Command Result.</returns>
        public CommandResult Insert(InsertEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                var result = this._Repository.Insert(criteria);
                return result;
            }

            return null;
        }

        /// <summary>
        /// Function to update a employee.
        /// </summary>
        /// <param name="criteria">UpdateEmployee Criteria.</param>
        /// <returns>Command Result.</returns>
        public CommandResult Update(UpdateEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                var result = this._Repository.Update(criteria);
                return result;
            }

            return null;
        }
    }
}
