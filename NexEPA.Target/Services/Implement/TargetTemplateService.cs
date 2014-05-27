// --------------------------------------------------------------------------
// <copyright file="TargetTemplateService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Model;
    using NexEPA.Target.Criteria.TargetTemplate;
    using NexEPA.Target.Models;
    using NexEPA.Target.Repositories.Implement;
    using NexEPA.Target.Repositories.Interface;
    using NexEPA.Target.Services.Interface;

    /// <summary>
    /// Target Template Service.
    /// </summary>
    public class TargetTemplateService : BaseService, ITargetTemplateService
    {
        private ITargetTemplateRepository _targetTemplateRepository;

        public TargetTemplateService(ITargetTemplateRepository targetTemplateRepository = null)
        {
            _targetTemplateRepository = targetTemplateRepository ?? new TargetTemplateRepository();
        }

        public TargetTemplateModel GetTargetTemplateById(GetTargetTemplateByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            TargetTemplateModel targetTemplate = null;
            if (criteria.IsValid())
            {
                targetTemplate = this.GetDataEntity<TargetTemplateModel>(criteria);
            }

            return targetTemplate;
        }

        public IEnumerable<TargetTemplateModel> GetAllTargetTemplate(GetAllTargetTemplateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<TargetTemplateModel> targetTemplates = null;
            if (criteria.IsValid())
            {
                targetTemplates = GetData<TargetTemplateModel>(criteria);
            }

            return targetTemplates;
        }

        public CommandResult InsertTargetTemplate(AddTargetTemplateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult UpdateTargetTemplate(UpdateTargetTemplateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = this._Repository.Update(criteria);
            }

            return result;
        }

        public CommandResult DeleteTargetTemplate(DeleteTargetTemplateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }

        public CommandResult AddKPIGroupWeight(AddKPIGroupWeightCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public IEnumerable<TargetTemplateKPIModel> GetListKPIByTargetTemplateId(GetListKPIByTargetTemplateIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<TargetTemplateKPIModel> kpis = null;
            if (criteria.IsValid())
            {
                kpis = GetData<TargetTemplateKPIModel>(criteria);
            }

            return kpis;
        }

        public IEnumerable<KPIGroupModel> GetSelectedKPIGroup(GetSelectedKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<KPIGroupModel> groups = null;
            if (criteria.IsValid())
            {
                groups = GetData<KPIGroupModel>(criteria);
            }

            return groups;
        }

        /// <summary>
        /// Update Target Template Status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateTargetTemplateStatus(UpdateTargetTemplateStatusCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;

            if (criteria.IsValid())
            {
                result = _Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get Target Setting.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>TargetSetting Models.</returns>
        public IEnumerable<TargetSettingModel> GetTargetSetting(GetTargetSettingCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<TargetSettingModel> targetSettings = null;
            if (criteria.IsValid())
            {
                targetSettings = _targetTemplateRepository.GetTargetSetting(criteria);
            }

            return targetSettings;
        }

        /// <summary>
        /// Set Default Target Template.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command results.</returns>
        public CommandResult SetDefaultTargetTemplate(SetTargetSettingCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = _Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Method Remove KPI Group.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Results.</returns>
        public CommandResult RemoveKPIGroup(RemoveKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = _Repository.Delete(criteria);
            }

            return result;
        }
    }
}
