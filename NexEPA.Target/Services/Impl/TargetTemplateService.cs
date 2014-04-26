// --------------------------------------------------------------------------
// <copyright file="TargetTemplateService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Services.Impl
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Target.Criteria.TargetTemplate;
    using NexEPA.Target.Models;
    using NexEPA.Target.Services.Interfaces;

    /// <summary>
    /// Target Template Service.
    /// </summary>
    public class TargetTemplateService : BaseService, ITargetTemplateService
    {
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
            IEnumerable<TargetTemplateModel> lstTargetTemplate = null;
            if (criteria.IsValid())
            {
                lstTargetTemplate = GetData<TargetTemplateModel>(criteria);
            }

            return lstTargetTemplate;
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

        public CommandResult AddKPIToTargetTemplate(AddKPIToTargetTemplateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult result = null;
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
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

        public IEnumerable<KPIModel> GetListKPIByTargetTemplateId(GetListKPIByTargetTemplateIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<KPIModel> lstKPI = null;
            if (criteria.IsValid())
            {
                lstKPI = GetData<KPIModel>(criteria);
            }

            return lstKPI;
        }
    }
}
