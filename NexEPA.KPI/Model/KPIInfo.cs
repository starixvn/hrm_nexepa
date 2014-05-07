// -----------------------------------------------------------------------
// <copyright file="KPIInfo.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Model
{
    using NexEPA.Core.Models;

    /// <summary>
    /// The information of KPI.
    /// </summary>
    public class KPIInfo
    {
        public int KPIId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsRequireComment { get; set; }

        public bool IsDisabled { get; set; }

        public int Score { get; set; }

        public int Percent 
        {
            get
            {
                return Score * 10;
            }
        }

        public string Comment { get; set; }

        public int Weight { get; set; }

        public string KPIGroupName { get; set; }

        public KPIGroupInfo Group { get; set; }

        public TargetType TargetType { get; set; }

        public bool CheckNullComment { get; set; }
    }
}
