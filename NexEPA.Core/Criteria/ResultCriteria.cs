// --------------------------------------------------------------------------
// <copyright file="ResultCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.11">Created</change>
// </history>

namespace NexEPA.Core.Criteria
{
    using Fanex.Data.Repository;

    /// <summary>
    /// Result Criteria.
    /// </summary>
    public class ResultCriteria : CriteriaBase
    {
        public ResultCriteria()
        {
            ErrCode = 0;
            ErrMsg = string.Empty;
        }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
