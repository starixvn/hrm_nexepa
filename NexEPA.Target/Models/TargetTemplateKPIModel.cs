// --------------------------------------------------------------------------
// <copyright file="TargetTemplateKPIModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.8">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    /// <summary>
    /// KPI Model.
    /// </summary>
    public class TargetTemplateKPIModel
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public string KPIGroupName { get; set; }

        public int KPIGroupId { get; set; }

        public int Weight { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
