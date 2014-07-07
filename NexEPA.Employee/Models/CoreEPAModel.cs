// -----------------------------------------------------------------------
// <copyright file="CoreEPAModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.07.07">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The core EPA model.
    /// </summary>
    public class CoreEPAModel
    {
        public CoreEPAModel()
        {
            KPIItems = new List<KPIModel>();
        }

        public int KPIGroupId { get; set; }

        public int KPIGroupWeight { get; set; }

        public bool IsSpreadKPI { get; set; }

        public List<KPIModel> KPIItems { get; set; }
    }
}
