// --------------------------------------------------------------------------
// <copyright file="TreeViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.6">Created</change>
// </history>

namespace NexEPA.Core.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// TreeView Model.
    /// </summary>
    public class TreeViewModel
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public List<TreeViewModel> Children { get; set; }
    }
}
