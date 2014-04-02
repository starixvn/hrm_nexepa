// -----------------------------------------------------------------------
// <copyright file="IKPIGroupRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.20">Created</change>
// </history>

namespace NexEPA.KPI.Repository.Interface.KPIGroup
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.KPI.Model.KPIGroup;

    /// <summary>
    /// The interface of KPI Group repository..
    /// </summary>
    public interface IKPIGroupRepository
    {
        IList<KPIGroupInfo> GetAllKPIGroup(ICriteria criteria);

        KPIGroupInfo GetKPIGroupById(ICriteria criteria);
    }
}
