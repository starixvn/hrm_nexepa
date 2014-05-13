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
    using NexEPA.Target.Models.Target;

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

        CommandResult AddKPIGroupWeight(AddKPIGroupWeightCriteria criteria);

        IEnumerable<TargetTemplateKPIModel> GetListKPIByTargetTemplateId(GetListKPIByTargetTemplateIdCriteria criteria);

        IEnumerable<TargetKPIGroup> GetSelectedKPIGroup(GetSelectedKPIGroupCriteria criteria);

        CommandResult UpdateTargetTemplateStatus(UpdateTargetTemplateStatusCriteria criteria);

        IEnumerable<TargetSettingModel> GetTargetSetting(GetTargetSettingCriteria criteria);

        CommandResult SetDefaultTargetTemplate(SetTargetSettingCriteria criteria);
    }
}
