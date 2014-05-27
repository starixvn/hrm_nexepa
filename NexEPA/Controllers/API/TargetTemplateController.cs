// --------------------------------------------------------------------------
// <copyright file="TargetTemplateController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.Model;
    using NexEPA.KPI.Services.Implement;
    using NexEPA.KPI.Services.Interface;
    using NexEPA.KPI.ViewModels;
    using NexEPA.Target.Criteria.TargetTemplate;
    using NexEPA.Target.Models;
    using NexEPA.Target.Services.Implement;
    using NexEPA.Target.Services.Interface;
    using NexEPA.Target.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Target Template Controller.
    /// </summary>
    public class TargetTemplateController : BaseController
    {
        private readonly ITargetTemplateService _targetTemplateService;
        private readonly IEPAService _epaService;
        private readonly IKPIService _kpiServices;
        private readonly IKPIGroupService _kpiGroupServices;

        public TargetTemplateController()
            : this(null, null, null, null)
        {
        }

        public TargetTemplateController(
            ITargetTemplateService targetTemplateService,
            IEPAService epaService,
            IKPIService kpiServices,
            IKPIGroupService kpiGroupServices)
        {
            this._targetTemplateService = targetTemplateService ?? new TargetTemplateService();
            _epaService = epaService ?? new EPAService();
            _kpiServices = kpiServices ?? new KPIService();
            _kpiGroupServices = kpiGroupServices ?? new KPIGroupService();
        }

        /// <summary>
        /// Method Get All Target Template.
        /// </summary>
        /// <param name="targetTypeId">Target Type Id.</param>
        /// <param name="isDisabled">Is disabled or not.</param>
        /// <returns>Target Template Model.</returns>
        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetTemplateModel> GetAllTargetTemplate(int? targetTypeId = null, bool? isDisabled = null)
        {
            IEnumerable<TargetTemplateModel> targetTemplates = null;
            var getAllTargetTemplateCriteria = new GetAllTargetTemplateCriteria
            {
                TargetTypeId = targetTypeId,
                IsDisabled = isDisabled
            };
            targetTemplates = this._targetTemplateService.GetAllTargetTemplate(getAllTargetTemplateCriteria);

            return targetTemplates;
        }

        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetType> GetAllTargetType()
        {
            IEnumerable<TargetType> targetTypes = null;
            var getAllTargetTypeCriteria = new GetAllTargetTypeCriteria();
            targetTypes = _epaService.GetAllTargetType(getAllTargetTypeCriteria);

            return targetTypes;
        }

        [ApiAuthorize(Roles = "SA")]
        public TargetTemplateModel GetTargetTemplateById(int targetTemplateId)
        {
            TargetTemplateModel targetTemplate = null;
            var getTargetTemplateByIdCriteria = new GetTargetTemplateByIdCriteria
            {
                TargetTemplateId = targetTemplateId
            };
            targetTemplate = this._targetTemplateService.GetTargetTemplateById(getTargetTemplateByIdCriteria);

            return targetTemplate;
        }

        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetTemplateKPIModel> GetSelectedKPIByTargetTemplate(int targetTemplateId)
        {
            IEnumerable<TargetTemplateKPIModel> selectedKPIs = null;
            var getListKPIByTargetTemplateIdCriteria = new GetListKPIByTargetTemplateIdCriteria()
            {
                TargetTemplateId = targetTemplateId
            };
            selectedKPIs = _targetTemplateService.GetListKPIByTargetTemplateId(getListKPIByTargetTemplateIdCriteria);

            return selectedKPIs;
        }

        /// <summary>
        /// Method Get KPI.
        /// </summary>
        /// <param name="onlySelected">Filter only selected KPI.</param> 
        /// <param name="targetTemplateId">Target Template Id.</param>
        /// <param name="targetTypeId">Target Type Id.</param>
        /// <param name="groupId">KPI Group Id.</param>
        /// <returns>List KPI item after group by KPIGroupName.</returns>
        [ApiAuthorize(Roles = "SA")]
        public List<TargetTemplateViewModel> GetKPI(
            bool onlySelected, 
            int? targetTemplateId = null, 
            int? targetTypeId = null, 
            int? groupId = null)
        {
            List<TargetTemplateViewModel> targetTemplateVMs = null;
            IEnumerable<KPIViewModel> kpis = null;
            var getAllKPIByKPIGroupCriteria = new GetAllKPIByKPIGroupCriteria()
            {
                TargetTemplateId = targetTemplateId.Value
            };
            kpis = _kpiServices.GetAllKPIByKPIGroup(getAllKPIByKPIGroupCriteria);

            if (onlySelected)
            {
                kpis = kpis.Where(x => x.Weight >= 0).ToList();
            }
            else
            {
                foreach (var item in kpis)
                {
                    if (item.Weight >= 0)
                    {
                        item.Selected = true;
                    }
                }
            }

            targetTemplateVMs = (from kpi in kpis
                    group kpi by kpi.KPIGroupName into item
                    select new TargetTemplateViewModel
                    {
                        KPIGroupName = item.Key,
                        KPIGroupItems = item.ToList()
                    }).ToList();

            return targetTemplateVMs;
        }

        /// <summary>
        /// Get Selected KPIGroup By Target Template.
        /// </summary>
        /// <param name="targetTemplateId">TargetTemplate Id.</param>
        /// <returns>List KPIGroup Items.</returns>
        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<KPIGroupModel> GetSelectedKPIGroupByTargetTemplate(int targetTemplateId)
        {
            var getSelectedKPIGroupCriteria = new GetSelectedKPIGroupCriteria() { TargetTemplateId = targetTemplateId };
            IEnumerable<KPIGroupModel> selectedGroups = _targetTemplateService.GetSelectedKPIGroup(getSelectedKPIGroupCriteria);

            if (selectedGroups != null)
            {
                foreach (var item in selectedGroups)
                {
                    if (item.Weight >= 0)
                    {
                        item.Selected = true;
                    }
                }
            }

            return selectedGroups;
        }

        /// <summary>
        /// Method Insert Target Template.
        /// </summary>
        /// <param name="model">Target Template model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(Roles = "SA")]
        public HttpResponseMessage Post(TargetTemplateModel model)
        {
            if (ModelState.IsValid)
            {
                var addTargetTemplateCriteria = new AddTargetTemplateCriteria()
                {
                    Name = model.Name,
                    TargetTypeId = model.TargetTypeId,
                    Description = model.Description,
                    CreatedDate = DateTime.Now,
                    CreatedBy = this.CurrentUser.UserId
                };

                CommandResult result = this._targetTemplateService.InsertTargetTemplate(addTargetTemplateCriteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
        }

        /// <summary>
        /// Method Update Target Template.
        /// </summary>
        /// <param name="model">Target Template model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(Roles = "SA")]
        public HttpResponseMessage Put(TargetTemplateModel model)
        {
            if (ModelState.IsValid)
            {
                var updateTargetTemplateCriteria = new UpdateTargetTemplateCriteria()
                {
                    TargetTemplateId = model.TargetTemplateId,
                    Name = model.Name,
                    TargetTypeId = model.TargetTypeId,
                    Description = model.Description,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = this.CurrentUser.UserId,
                    KPIs = model.ConvertKPIItemToXML
                };

                CommandResult result = this._targetTemplateService.UpdateTargetTemplate(updateTargetTemplateCriteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
        }

        /// <summary>
        /// Method Delete Target Template.
        /// </summary>
        /// <param name="targetTemplateId">Target Template Id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(Roles = "SA")]
        public HttpResponseMessage Delete(int targetTemplateId)
        {
            var deleteTargetTemplateCriteria = new DeleteTargetTemplateCriteria()
            {
                TargetTemplateId = targetTemplateId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };
            CommandResult result = this._targetTemplateService.DeleteTargetTemplate(deleteTargetTemplateCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Method Add KPI Group Weight.
        /// </summary>
        /// <param name="model">TargetKPIGroupWeight Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(Roles = "SA")]
        public HttpResponseMessage AddKPIGroupWeight(TargetKPIGroupWeightModel model)
        {
            var addKPIGroupWeightCriteria = new AddKPIGroupWeightCriteria()
            {
                TargetTemplateId = model.TargetTemplateId,
                KPIGroups = model.ConvertKPIGroupWeightToXML,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };
            CommandResult result = this._targetTemplateService.AddKPIGroupWeight(addKPIGroupWeightCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update Target Template Status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        [ApiAuthorize(Roles = "SA")]
        [HttpPut]
        public CommandResult UpdateTargetTemplateStatus(UpdateTargetTemplateStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _targetTemplateService.UpdateTargetTemplateStatus(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetSettingModel> GetTargetSetting()
        {
            IEnumerable<TargetSettingModel> targetSettings = null;
            var getTargetSettingCriteria = new GetTargetSettingCriteria();
            targetSettings = _targetTemplateService.GetTargetSetting(getTargetSettingCriteria);

            return targetSettings;
        }

        /// <summary>
        /// Method Set Default Target Template.
        /// </summary>
        /// <param name="model">Model contains target id and target template id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(Roles = "SA")]
        public HttpResponseMessage SetDefaultTargetTemplate(dynamic model)
        {
            var setTargetSettingCriteria = new SetTargetSettingCriteria()
            {
                TargetTypeId = model.targetTypeId,
                TargetTemplateId = model.targetTemplateId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };
            CommandResult result = this._targetTemplateService.SetDefaultTargetTemplate(setTargetSettingCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Method Remove KPI Group.
        /// </summary>
        /// <param name="model">Required Data to remove Group.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage RemoveKPIGroup(dynamic model)
        {
            var removeKPIGroupCriteria = new RemoveKPIGroupCriteria()
            {
                KPIGroupId = model.groupId,
                TargetTemplateId = model.targetTemplateId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = this._targetTemplateService.RemoveKPIGroup(removeKPIGroupCriteria);
            if (result.Code != 0)
            {
                List<string> errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            IEnumerable<string> errors = null;
            errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));

            return errors;
        }
    }
}
