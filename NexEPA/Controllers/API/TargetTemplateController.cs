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
        private const string _KPIGroup = "<KPIGroup KPIGroupId='{0}' Weight='{1}' IsSpreadKPI='{2}'></KPIGroup>";
        private const string _KPIItem = "<KPI KPIId='{0}' KPIGroupId='{1}' Weight='{2}'></KPI>";
        private const string _BeginRootTag = "<ROOT>";
        private const string _EndRootTag = "</ROOT>";
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

        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetTemplateModel> Get(int? targetTypeId = null, bool? isDisabled = null)
        {
            GetAllTargetTemplateCriteria criteria = new GetAllTargetTemplateCriteria
            {
                TargetTypeId = targetTypeId,
                IsDisabled = isDisabled
            };

            var data = this._targetTemplateService.GetAllTargetTemplate(criteria);
            return data;
        }

        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetType> GetAllTargetType()
        {
            GetAllTargetTypeCriteria criteria = new GetAllTargetTypeCriteria();
            var data = _epaService.GetAllTargetType(criteria);
            return data;
        }

        [ApiAuthorize(Roles = "SA")]
        public TargetTemplateModel GetTargetTemplateById(int targetTemplateId)
        {
            GetTargetTemplateByIdCriteria criteria = new GetTargetTemplateByIdCriteria
            {
                TargetTemplateId = targetTemplateId
            };

            var data = this._targetTemplateService.GetTargetTemplateById(criteria);
            return data;
        }

        [ApiAuthorize(Roles = "SA")]
        public IEnumerable<TargetTemplateKPIModel> GetSelectedKPIByTargetTemplate(int targetTemplateId)
        {
            var kpiTargetTemplateCriteria = new GetListKPIByTargetTemplateIdCriteria()
            {
                TargetTemplateId = targetTemplateId
            };

            IEnumerable<TargetTemplateKPIModel> selectedKPIs = _targetTemplateService.GetListKPIByTargetTemplateId(kpiTargetTemplateCriteria);
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
        public TargetTemplateViewModel GetKPI(bool onlySelected, int? targetTemplateId = null, int? targetTypeId = null, int? groupId = null)
        {
            dynamic data = null;
            var getAllKPIByKPIGroupCriteria = new GetAllKPIByKPIGroupCriteria()
            {
                TargetTemplateId = targetTemplateId.Value
            };
            IEnumerable<KPIViewModel> lstKPI = _kpiServices.GetAllKPIByKPIGroup(getAllKPIByKPIGroupCriteria);

            if (onlySelected)
            {
                lstKPI = lstKPI.Where(x => x.Weight >= 0).ToList();
            }
            else
            {
                foreach (var item in lstKPI)
                {
                    if (item.Weight >= 0)
                    {
                        item.Selected = true;
                    }
                }
            }

            data = (from kpi in lstKPI
                    group kpi by kpi.KPIGroupName into item
                    select new TargetTemplateViewModel
                    {
                        KPIGroupName = item.Key,
                        KPIGroupItems = item.ToList()
                    }).ToList();

            return data;
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
                AddTargetTemplateCriteria criteria = new AddTargetTemplateCriteria()
                {
                    Name = model.Name,
                    TargetTypeId = model.TargetTypeId,
                    Description = model.Description,
                    CreatedDate = DateTime.Now,
                    CreatedBy = this.CurrentUser.UserId
                };

                CommandResult result = this._targetTemplateService.InsertTargetTemplate(criteria);
                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
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
                UpdateTargetTemplateCriteria criteria = new UpdateTargetTemplateCriteria()
                {
                    TargetTemplateId = model.TargetTemplateId,
                    Name = model.Name,
                    TargetTypeId = model.TargetTypeId,
                    Description = model.Description,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = this.CurrentUser.UserId,
                    KPIs = ConvertKPIItemToXML(model.KPIIds)
                };

                CommandResult result = this._targetTemplateService.UpdateTargetTemplate(criteria);
                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
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
            DeleteTargetTemplateCriteria criteria = new DeleteTargetTemplateCriteria()
            {
                TargetTemplateId = targetTemplateId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };
            CommandResult result = this._targetTemplateService.DeleteTargetTemplate(criteria);
            if (result.Code != 0)
            {
                List<string> errorMessage = new List<string>() { result.Message };
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
            string dataKPIGroupWeightXML = ConvertKPIGroupWeightToXML(model.KPIGroups);
            AddKPIGroupWeightCriteria criteria = new AddKPIGroupWeightCriteria()
            {
                TargetTemplateId = model.TargetTemplateId,
                KPIGroups = dataKPIGroupWeightXML,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };
            CommandResult result = this._targetTemplateService.AddKPIGroupWeight(criteria);
            if (result.Code != 0)
            {
                List<string> errorMessage = new List<string>() { result.Message };
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
            var getTargetSettingCriteria = new GetTargetSettingCriteria();
            var targetSettings = _targetTemplateService.GetTargetSetting(getTargetSettingCriteria);

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
            var criteria = new SetTargetSettingCriteria()
            {
                TargetTypeId = model.targetTypeId,
                TargetTemplateId = model.targetTemplateId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };
            CommandResult result = this._targetTemplateService.SetDefaultTargetTemplate(criteria);
            if (result.Code != 0)
            {
                List<string> errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private string ConvertKPIGroupWeightToXML(List<KPIGroupModel> kpiGroups)
        {
            var value = new StringBuilder(_BeginRootTag);
            foreach (var item in kpiGroups)
            {
                var spreadKPI = item.IsSpreadKPI == true ? 1 : 0;
                value.AppendFormat(_KPIGroup, item.KPIGroupId, item.Weight, spreadKPI);
            }

            return value.Append(_EndRootTag).ToString();
        }

        private string ConvertKPIItemToXML(List<TargetTemplateKPIModel> kpis)
        {
            var value = new StringBuilder(_BeginRootTag);
            foreach (var item in kpis)
            {
                value.AppendFormat(_KPIItem, item.KPIId, item.KPIGroupId, item.Weight);
            }

            return value.Append(_EndRootTag).ToString();
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
