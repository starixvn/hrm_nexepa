// -----------------------------------------------------------------------
// <copyright file="ReportKPIInformationModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    /// <summary>
    /// The KPI Information Model.
    /// </summary>
    public class ReportKPIInformationModel
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public int Score { get; set; }
    }
}
