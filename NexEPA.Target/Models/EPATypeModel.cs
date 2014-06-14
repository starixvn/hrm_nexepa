// -----------------------------------------------------------------------
// <copyright file="EPATypeModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.14">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    /// <summary>
    /// The EPA type model.
    /// </summary>
    public class EPATypeModel
    {
        public int EPATypeId { get; set; }

        public string Name { get; set; }

        public int TargetTypeId { get; set; }

        public int ScheduleRunType { get; set; }
    }
}
