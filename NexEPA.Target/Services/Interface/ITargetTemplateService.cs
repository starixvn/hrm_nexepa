// --------------------------------------------------------------------------
// <copyright file="ITargetTemplateService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Target.Criteria.TargetEmployeeSetting;
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

        CommandResult AddKPIGroupWeight(AddKPIGroupWeightCriteria criteria);

        IEnumerable<TargetTemplateKPIModel> GetListKPIByTargetTemplateId(GetListKPIByTargetTemplateIdCriteria criteria);

        IEnumerable<KPIGroupModel> GetSelectedKPIGroup(GetSelectedKPIGroupCriteria criteria);

        CommandResult UpdateTargetTemplateStatus(UpdateTargetTemplateStatusCriteria criteria);

        IEnumerable<TargetSettingModel> GetTargetSetting(GetTargetSettingCriteria criteria);

        CommandResult SetDefaultTargetTemplate(SetTargetSettingCriteria criteria);

        CommandResult RemoveKPIGroup(RemoveKPIGroupCriteria criteria);

        IEnumerable<TargetEmployeeSettingModel> GetTargetEmployeeSetting(GetTargetEmployeeSettingCriteria criteria);

        IEnumerable<EPATypeModel> GetEPAType(GetEPATypeCriteria criteria);

        CommandResult UpdateTargetEmployeeSetting(UpdateTargeEmployeeSettingCriteria criteria);
    }
}
