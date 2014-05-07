// --------------------------------------------------------------------------
// <copyright file="IViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.5.7">Created</change>
// </history>

namespace NexEPA.Core.ViewModels
{
    using NexEPA.Core.Models;

    public interface IViewModel
    {
        void SetInfo(IModel model);
    }
}
