// -----------------------------------------------------------------------
// <copyright file="DashboardModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.14">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// The model of dashboard will return to view.
    /// </summary>
    public class DashboardModel
    {
        public GeneralInformationOfProjectModel GeneralInformation { get; set; }

        public IEnumerable<OpenProjectModel> OpenProjects { get; set; }

        public IEnumerable<HistoryProjectViewModel> HistoryProjects { get; set; }

        public IEnumerable<HistoryProjectModel> RawHistoryProjects { get; set; }

        public IEnumerable<KPIModel> ListKPIs { get; set; }
    }
}
