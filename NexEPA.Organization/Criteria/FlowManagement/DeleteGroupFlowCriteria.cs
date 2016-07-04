// --------------------------------------------------------------------------
// <copyright file="DeleteGroupFlowCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Criteria.FlowManagement
{
    using System;
    using Fanex.Data.Repository;

    /// <summary>
    /// Delete Group Flow Criteria.
    /// </summary>
    public class DeleteGroupFlowCriteria : CriteriaBase
    {
        public int GroupId { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return GroupId > 0;
        }

        public override string GetSettingKey()
        {
            return "SettingFlowGroup_Delete";
        }
    }
}
