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
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Model.Target;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Services.Implement.KPI;
    using NexEPA.KPI.Services.Interface.KPI;
    using NexEPA.KPI.ViewModel.KPI;
    using NexEPA.Target.Criteria.TargetTemplate;
    using NexEPA.Target.Models;
    using NexEPA.Target.Services.Impl;
    using NexEPA.Target.Services.Interfaces;

    /// <summary>
    /// Target Template Controller.
    /// </summary>
    public class TargetTemplateController : ApiController
    {
        private readonly ITargetTemplateService _targetTemplateService;
        private readonly EPAService _epaService;
        private readonly IKPIServices _kpiServices;

        public TargetTemplateController()
            : this(null, null, null)
        {
        }

        public TargetTemplateController(ITargetTemplateService targetTemplateService, EPAService epaService, IKPIServices kpiServices)
        {
            this._targetTemplateService = targetTemplateService ?? new TargetTemplateService();
            _epaService = epaService ?? new EPAService();
            _kpiServices = kpiServices ?? new KPIServices();
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

        /// <summary>
        /// Method Get KPI.
        /// </summary>
        /// <param name="targetTypeId">Target Type Id.</param>
        /// <param name="groupId">KPI Group Id.</param>
        /// <returns>List KPI item after group by KPIGroupName.</returns>
        public dynamic GetKPI(int? targetId = null, int? targetTypeId = null, int? groupId = null)
        {
            List<KPIItem> lstKPI = _kpiServices.GetKPI(targetTypeId, groupId);

            if (targetId != null)
            {
                var kpiTargetTemplateCriteria = new GetListKPIByTargetTemplateIdCriteria() { TargetId = targetId };
                List<KPIModel> lstSelectedKPI = _targetTemplateService.GetListKPIByTargetTemplateId(kpiTargetTemplateCriteria).ToList();
                if (lstSelectedKPI != null)
                {
                    foreach (var item in lstKPI.Where(x => lstSelectedKPI.Select(y => y.KPIId).Contains(x.KPIId)))
                    {
                        item.Selected = true;
                    }
                }
            }

            var data = (from kpi in lstKPI
                        group kpi by kpi.KPIGroupName into item
                        select new
                        {
                            KPIGroupName = item.Key,
                            KPIGroupItems = item.ToList()
                        }).ToList();

            return data;
        }

        public IEnumerable<TargetTemplateModel> Get()
        {
            GetAllTargetTemplateCriteria criteria = new GetAllTargetTemplateCriteria();
            var data = this._targetTemplateService.GetAllTargetTemplate(criteria);
            return data;
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
                    CreatedBy = 1
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
                    TargetId = model.TargetId,
                    Name = model.Name,
                    TargetTypeId = model.TargetTypeId,
                    Description = model.Description,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = 1
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
                ModifiedBy = 1,
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
                    AddedBy = 1
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

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
