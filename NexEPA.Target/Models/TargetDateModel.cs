// -----------------------------------------------------------------------
// <copyright file="TargetDateModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.15">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System;

    /// <summary>
    /// Target Date model.
    /// </summary>
    public class TargetDateModel
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
