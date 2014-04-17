// -----------------------------------------------------------------------
// <copyright file="TargetKPIItem.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.17">Created</change>
// </history>

namespace NexEPA.Target.ViewModels.Target
{
    /// <summary>
    /// The Target KPI Item view model.
    /// </summary>
    public class TargetKPIItem
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
