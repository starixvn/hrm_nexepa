// --------------------------------------------------------------------------
// <copyright file="GroupingKPIModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using NexEPA.KPI.Model;

    /// <summary>
    /// Grouping KPI Model.
    /// </summary>
    public class GroupingKPIModel
    {
        public string KPIGroupName { get; set; }

        public List<KPIModel> KPIGroupItems { get; set; }

        public int KPIGroupWeight
        {
            get
            {
                if (KPIGroupItems != null)
                {
                    var firstItem = KPIGroupItems.FirstOrDefault();
                    return firstItem.Group.Weight.Value;
                }

                return 0;
            }
        }

        public bool IsSpreadKPI
        {
            get
            {
                if (KPIGroupItems.Any())
                {
                    return KPIGroupItems[0].Group.IsSpreadKPI;
                }

                return false;
            }
        }
    }
}
