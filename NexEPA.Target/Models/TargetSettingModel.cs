// --------------------------------------------------------------------------
// <copyright file="TargetSettingModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.9">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using NexEPA.Core.Models;

    /// <summary>
    /// Target Setting Model.
    /// </summary>
    public class TargetSettingModel
    {
        public int TargetTypeId { get; set; }

        public string Name { get; set; }

        public TargetTemplateModel TargetTemplate { get; set; }
    }
}
