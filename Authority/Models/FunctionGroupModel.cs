// -----------------------------------------------------------------------
// <copyright file="FunctionGroupModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.05">Created</change>
// </history>

namespace NexEPA.Authority.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The Function Group Model.
    /// </summary>
    public class FunctionGroupModel : FunctionModel
    {
        public FunctionGroupModel()
        {
            ChildFunctions = new List<FunctionModel>();
        }

        public List<FunctionModel> ChildFunctions { get; set; }
    }
}
