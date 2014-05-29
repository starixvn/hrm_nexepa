// --------------------------------------------------------------------------
// <copyright file="TargetKPIGroupWeightModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.26">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using System.Text;
    using NexEPA.KPI.Model;

    /// <summary>
    /// Target KPI Group Weight.
    /// </summary>
    public class TargetKPIGroupWeightModel
    {
        private const string _KPIGroup = "<KPIGroup KPIGroupId='{0}' Weight='{1}' IsSpreadKPI='{2}' DisplayOrder='{3}'></KPIGroup>";
        private const string _BeginRootTag = "<ROOT>";
        private const string _EndRootTag = "</ROOT>";

        public int TargetTemplateId { get; set; }

        public List<KPIGroupModel> KPIGroups { get; set; }

        public string ConvertKPIGroupWeightToXML
        {
            get
            {
                if (KPIGroups != null)
                {
                    var value = new StringBuilder(_BeginRootTag);
                    foreach (KPIGroupModel item in KPIGroups)
                    {
                        var spreadKPI = item.IsSpreadKPI == true ? 1 : 0;
                        value.AppendFormat(_KPIGroup, item.KPIGroupId, item.Weight, spreadKPI, item.DisplayOrder);
                    }

                    return value.Append(_EndRootTag).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
