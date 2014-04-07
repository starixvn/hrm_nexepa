// --------------------------------------------------------------------------
// <copyright file="TargetTemplateModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System;

    /// <summary>
    /// Target Template Model.
    /// </summary>
    public class TargetTemplateModel
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TargetTypeId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }
    }
}
