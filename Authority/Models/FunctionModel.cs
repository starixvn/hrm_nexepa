// -----------------------------------------------------------------------
// <copyright file="FunctionModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Authority.Models
{
    /// <summary>
    /// The function model.
    /// </summary>
    public class FunctionModel
    {
        public int FunctionId { get; set; }

        public string FunctionKey { get; set; }

        public string Name { get; set; }

        public int ParentFunctionId { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
