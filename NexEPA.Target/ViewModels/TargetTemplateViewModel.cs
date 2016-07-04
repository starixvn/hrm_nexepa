// --------------------------------------------------------------------------
// <copyright file="TargetTemplateViewModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.17">Created</change>
// </history>

namespace NexEPA.Target.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using NexEPA.KPI.ViewModels;

    /// <summary>
    /// Target Template ViewModel.
    /// </summary>
    public class TargetTemplateViewModel
    {
        public string KPIGroupName { get; set; }

        public List<KPIViewModel> KPIGroupItems { get; set; }

        public bool IsSpreadKPI
        {
            get
            {
                if (KPIGroupItems.Any())
                {
                    return KPIGroupItems[0].IsSpreadKPI;
                }

                return false;
            }
        }

        public int Id
        {
            get
            {
                if (KPIGroupItems.Any())
                {
                    return KPIGroupItems[0].KPIGroupId;
                }

                return 0;
            }
        }
    }
}
