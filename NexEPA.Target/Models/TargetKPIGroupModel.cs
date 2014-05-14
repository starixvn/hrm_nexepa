// -----------------------------------------------------------------------
// <copyright file="TargetKPIGroupModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.17">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The Target KPI Group Item view model.
    /// </summary>
    public class TargetKPIGroupModel
    {
        public TargetKPIGroupModel()
        {
            KPIs = new List<KPIModel>();
            Group = new KPIGroupModel();
        }

        public KPIGroupModel Group { get; set; }

        public List<KPIModel> KPIs { get; set; }
    }
}
