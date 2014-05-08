// -----------------------------------------------------------------------
// <copyright file="JobTitle.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.08">Created</change>
// </history>

namespace NexEPA.Core.Models
{
    /// <summary>
    /// The Job Title model.
    /// </summary>
    public class JobTitle : IModel
    {
        public int? JobTitleId { get; set; }

        public string Name { get; set; }
    }
}
