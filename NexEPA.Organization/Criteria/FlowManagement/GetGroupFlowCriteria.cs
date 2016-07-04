// --------------------------------------------------------------------------
// <copyright file="GetGroupFlowCriteria.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Criteria.FlowManagement
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Get Group Flow Criteria.
    /// </summary>
    public class GetGroupFlowCriteria : CriteriaBase
    {
        public int GroupId { get; set; }

        public override bool IsValid()
        {
            return GroupId > 0;
        }

        public override string GetSettingKey()
        {
            return "SettingFlowGroup_Get";
        }
    }
}
