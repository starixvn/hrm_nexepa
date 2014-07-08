// -----------------------------------------------------------------------
// <copyright file="TemporaryEPAModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.07.07">Created</change>
// </history>

namespace NexEPA.Employee.Models
{
    /// <summary>
    /// The temporary EPA model.
    /// </summary>
    public class TemporaryEPAModel
    {
        public int EmployeeId { get; set; }

        public int KPIId { get; set; }

        public int Score { get; set; }

        public int Weight { get; set; }

        public string Comment { get; set; }

        public int KPIGroupId { get; set; }

        public int KPIGroupWeight { get; set; }

        public bool IsSpreadKPI { get; set; }
    }
}
