// -----------------------------------------------------------------------
// <copyright file="EPAService.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.Core.Services
{
    using System;
    using System.Collections.Generic;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Model.Target;

    /// <summary>
    /// EPA services.
    /// </summary>
    public class EPAService : BaseService
    {
        /// <summary>
        /// Get all Target Type.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List Target Type.</returns>
        public IEnumerable<TargetType> GetAllTargetType(GetAllTargetTypeCriteria criteria) 
        {
            return GetData<TargetType>(criteria);
        }

        /// <summary>
        /// Get status by ID.
        /// </summary>
        /// <param name="statusId">The status ID.</param>
        /// <returns>The status.</returns>
        public string GetStatusById(int statusId)
        {
            string status = string.Empty;

            switch (statusId)
            {
                case (int)Constants.Status.Active:
                    status = Constants.ActiveStatus;
                    break;

                case (int)Constants.Status.Pending:
                    status = Constants.PendingStatus;
                    break;

                case (int)Constants.Status.Closed:
                    status = Constants.ClosedStatus;
                    break;
            }

            return status;
        }

        /// <summary>
        /// Get all status.
        /// </summary>
        /// <returns>List status.</returns>
        public List<dynamic> GetAllStatus()
        {
            var statusIdList = Enum.GetValues(typeof(Constants.Status));
            List<dynamic> allStatus = new List<dynamic>();
 
            foreach (var status in statusIdList)
            {
                var statusId = (int)status;
                var statusName = GetStatusById(statusId);

                var statusItem = new
                {
                    Status = statusId,
                    Name = statusName
                };

                allStatus.Add(statusItem);
            }

            return allStatus;
        }
    }
}
