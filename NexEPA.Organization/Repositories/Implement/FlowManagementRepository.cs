// --------------------------------------------------------------------------
// <copyright file="FlowManagementRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.19">Created</change>
// </history>

namespace NexEPA.Organization.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Utilities;
    using NexEPA.Employee.Models;
    using NexEPA.Organization.Models;
    using NexEPA.Organization.Repositories.Interface;

    /// <summary>
    /// Flow Management Repository.
    /// </summary>
    public class FlowManagementRepository : IFlowManagementRepository
    {
        /// <summary>
        /// Method Get Flow Management.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>List EPAFlowModel.</returns>
        public IEnumerable<EPAFlowModel> GetFlowManagement(ICriteria criteria)
        {
            IEnumerable<EPAFlowModel> epaFlow = null;
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                epaFlow = db.Query<EPAFlowModel, EmployeeModel, EPAFlowModel>(
                   (f, e) =>
                   {
                       f.EvaluatorInfo = e;
                       return f;
                   },
                   "EvaluatorId",
                   criteria.GetSpParams());
            }

            foreach (var item in epaFlow)
            {
                if (item.EvaluatorInfo.Email != null)
                    {
                        item.EvaluatorInfo.Gravatar = GravatarImage.HashEmailForGravatar(item.EvaluatorInfo.Email);
                    }
            }

            return epaFlow;
        }
    }
}
