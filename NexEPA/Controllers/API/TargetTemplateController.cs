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
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Model.Target;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.Services.Implement.KPI;
    using NexEPA.KPI.Services.Implement.KPIGroup;
    using NexEPA.KPI.Services.Interface.KPI;
    using NexEPA.KPI.Services.Interface.KPIGroup;
    using NexEPA.KPI.ViewModel.KPI;
    using NexEPA.KPI.ViewModel.KPIGroup;
    using NexEPA.Target.Criteria.TargetTemplate;
    using NexEPA.Target.Models;
    using NexEPA.Target.Models.Target;
    using NexEPA.Target.Services.Impl;
    using NexEPA.Target.Services.Interfaces;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Target Template Controller.
    /// </summary>
    public class TargetTemplateController : BaseController
    {
        private const string _KPIGroup = "<KPIGroup KPIGroupId='{0}' Weight='{1}'></KPIGroup>";
        private const string _KPIItem = "<KPI KPIId='{0}' KPIGroupId='{1}' Weight='{2}'></KPI>";
        private const string _BeginRootTag = "<ROOT>";
        private const string _EndRootTag = "</ROOT>";
        private readonly ITargetTemplateService _targetTemplateService;
        private readonly EPAService _epaService;
        private readonly IKPIServices _kpiServices;
        private readonly IKPIGroupServices _kpiGroupServices;

        public TargetTemplateController()
            : this(null, null, null, null)
        {
        }

        public TargetTemplateController(
            ITargetTemplateService targetTemplateService,
            EPAService epaService,
            IKPIServices kpiServices,
            IKPIGroupServices kpiGroupServices)
        {
            this._targetTemplateService = targetTemplateService ?? new TargetTemplateService();
            _epaService = epaService ?? new EPAService();
            _kpiServices = kpiServices ?? new KPIServices();
            _kpiGroupServices = kpiGroupServices ?? new KPIGroupServices();
        }

        public IEnumerable<TargetTemplateModel> Get()
        {
            GetAllTargetTemplateCriteria criteria = new GetAllTargetTemplateCriteria();
            var data = this._targetTemplateService.GetAllTargetTemplate(criteria);
            return data;
        }

        public IEnumerable<TargetType> GetAllTargetType()
        {
            GetAllTargetTypeCriteria criteria = new GetAllTargetTypeCriteria();
            var data = _epaService.GetAllTargetType(criteria);
            return data;
        }

        public TargetTemplateModel GetTargetTemplateById(int targetId)
        {
            GetTargetTemplateByIdCriteria criteria = new GetTargetTemplateByIdCriteria() { TargetId = targetId };
            var data = this._targetTemplateService.GetTargetTemplateById(criteria);
            return data;
        }

        public IEnumerable<KPIModel> GetSelectedKPIByTargetTemplate(int targetTemplateId)
        {
            var kpiTargetTemplateCriteria = new GetListKPIByTargetTemplateIdCriteria() { TargetTemplateId = targetTemplateId };
            IEnumerable<KPIModel> selectedKPIs = _targetTemplateService.GetListKPIByTargetTemplateId(kpiTargetTemplateCriteria);
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
        public dynamic GetKPI(bool onlySelected, int? targetTemplateId = null, int? targetTypeId = null, int? groupId = null)
        {
            dynamic data = null;

            var getAllKPIByKPIGroupCriteria = new GetAllKPIByKPIGroupCriteria() { TargetTemplateId = targetTemplateId.Value };
            IEnumerable<KPIItem> lstKPI = _kpiServices.GetAllKPIByKPIGroup(getAllKPIByKPIGroupCriteria);

            var kpiTargetTemplateCriteria = new GetListKPIByTargetTemplateIdCriteria() { TargetTemplateId = targetTemplateId };
            List<KPIModel> lstSelectedKPI = _targetTemplateService.GetListKPIByTargetTemplateId(kpiTargetTemplateCriteria).ToList();
            if (lstSelectedKPI != null)
            {
                if (onlySelected)
                {
                    lstKPI = lstKPI.Where(x => lstSelectedKPI.Select(y => y.KPIId).Contains(x.KPIId)).ToList();
                    foreach (var item in lstKPI)
                    {
                        item.Selected = true;
                    }
                }
                else
                {
                    foreach (var item in lstKPI.Where(x => lstSelectedKPI.Select(y => y.KPIId).Contains(x.KPIId)))
                    {
                        var kpis = lstSelectedKPI.Where(x => x.KPIId == item.KPIId).ToList();
                        foreach (var kpi in kpis)
                        {
                            if (kpi.KPIGroupId == item.KPIGroupId)
                            {
                                item.Selected = true;
                                item.Weight = kpi.Weight;
                            }
                        }
                    }
                }
            }

            data = (from kpi in lstKPI
                    group kpi by kpi.KPIGroupName into item
                    select new
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
        public List<KPIGroupItem> GetSelectedKPIGroupByTargetTemplate(int targetTemplateId)
        {
            List<KPIGroupItem> allGroups = null;
            var getAllGroupCriteria = new GetAllKPIGroupCriteria { TargetTypeId = null };
            allGroups = _kpiGroupServices.GetAllKPIGroup(getAllGroupCriteria);

            if (allGroups != null)
            {
                IEnumerable<TargetKPIGroup> selectedGroups = null;
                var getSelectedKPIGroupCriteria = new GetSelectedKPIGroupCriteria() { TargetTemplateId = targetTemplateId };
                selectedGroups = _targetTemplateService.GetSelectedKPIGroup(getSelectedKPIGroupCriteria);

                if (selectedGroups != null)
                {
                    var duplicatedItems = allGroups.Where(x => selectedGroups.Select(y => y.KPIGroupId).Contains(x.KPIGroupID));
                    foreach (var item in duplicatedItems)
                    {
                        TargetKPIGroup kpiGroup = selectedGroups.Where(x => x.KPIGroupId == item.KPIGroupID).FirstOrDefault();
                        item.Selected = true;
                        item.Weight = kpiGroup.Weight;
                    }
                }
            }

            return allGroups;
        }

        /// <summary>
        /// Method Insert Target Template.
        /// </summary>
        /// <param name="model">Target Template model.</param>
        /// <returns>Http Response Message.</returns>
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
        /// <param name="targetId">Target Template Id.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage Delete(int targetId)
        {
            DeleteTargetTemplateCriteria criteria = new DeleteTargetTemplateCriteria()
            {
                TargetId = targetId,
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
        /// Add KPI to targetTemplate.
        /// </summary>
        /// <param name="model">Target Template KPI Model.</param>
        /// <returns>Http Response message.</returns>
        public HttpResponseMessage AddKPI(TargetTemplateKPIModel model)
        {
            if (model != null)
            {
                AddKPIToTargetTemplateCriteria criteria = new AddKPIToTargetTemplateCriteria()
                {
                    TargetId = model.TargetId,
                    KPIIds = model.KPIIds,
                    AddedDate = DateTime.Now,
                    AddedBy = this.CurrentUser.UserId
                };

                CommandResult result = this._targetTemplateService.AddKPIToTargetTemplate(criteria);
                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Method Add KPI Group Weight.
        /// </summary>
        /// <param name="model">TargetKPIGroupWeight Model.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage AddKPIGroupWeight(TargetKPIGroupWeight model)
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

        private string ConvertKPIGroupWeightToXML(List<TargetKPIGroup> kpiGroups)
        {
            var value = new StringBuilder(_BeginRootTag);
            foreach (var item in kpiGroups)
            {
                value.AppendFormat(_KPIGroup, item.KPIGroupId, item.Weight);
            }

            return value.Append(_EndRootTag).ToString();
        }

        private string ConvertKPIItemToXML(List<KPIModel> kpis)
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
