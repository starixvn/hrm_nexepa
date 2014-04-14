// --------------------------------------------------------------------------
// <copyright file="ITargetTemplateService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Services.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Target.Criteria.TargetTemplate;
    using NexEPA.Target.Models;

    /// <summary>
    /// Interface Target Template Service.
    /// </summary>
    public interface ITargetTemplateService
    {
        TargetTemplateModel GetTargetTemplateById(GetTargetTemplateByIdCriteria criteria);

        IEnumerable<TargetTemplateModel> GetAllTargetTemplate(GetAllTargetTemplateCriteria criteria);

        CommandResult InsertTargetTemplate(AddTargetTemplateCriteria criteria);

        CommandResult UpdateTargetTemplate(UpdateTargetTemplateCriteria criteria);

        CommandResult DeleteTargetTemplate(DeleteTargetTemplateCriteria criteria);

        CommandResult AddKPIToTargetTemplate(AddKPIToTargetTemplateCriteria criteria);

        IEnumerable<KPIModel> GetListKPIByTargetTemplateId(GetListKPIByTargetTemplateIdCriteria criteria);
    }
}
