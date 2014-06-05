// -----------------------------------------------------------------------
// <copyright file="GetAllFunctionCriteria.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Authority.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get all function criteria.
    /// </summary>
    public class GetAllFunctionCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "Function_GetAll";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
