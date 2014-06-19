// -----------------------------------------------------------------------
// <copyright file="ProcessPeriodModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.19">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    /// <summary>
    /// Process Period model.
    /// </summary>
    public class ProcessPeriodModel
    {
        public int ProcessId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public bool IsSelfEvaluated { get; set; }
    }
}
