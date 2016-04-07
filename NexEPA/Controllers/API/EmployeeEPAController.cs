namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MemberEPA;
    using NexEPA.MemberEPA.Models.MyEPA;
    using NexEPA.MemberEPA.Services.Implement;
    using NexEPA.MemberEPA.Services.Interface;
    using NexEPA.Target.Models;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// MyEPA Controller.
    /// </summary>
    public class EmployeeEPAController : BaseController
    {
        private const string _ShowFullEPACommentKey = "showFullEPAComment";
        private readonly IMemberEPAService memberEPAService;

        private ICommonService commonService;

        public EmployeeEPAController()
            : this(null, null)
        {
        }

        public EmployeeEPAController(IMemberEPAService memberEPAService, ICommonService commonService)
        {
            this.memberEPAService = memberEPAService ?? new MemberEPAService();
            this.commonService = commonService ?? new CommonService();
        }

        /// <summary>
        /// Add employee score.
        /// </summary>
        /// <param name="model">EPA Info Model.</param>
        /// <returns>Http Response.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageMemberEPA, Policy = Policy.Host)]
        [HttpPost]
        public HttpResponseMessage AddEmployeeEPA(EPAInfoModel model)
        {
            var criteria = new AddOrUpdateEPAScoreCriteria()
            {
                TargetId = model.TargetId,
                EmployeeId = model.EmployeeId,
                EvaluatorId = model.ProjectManagerId,
                Comment = model.Comment,
                Score = memberEPAService.Encode(model.FinalScore),
                KPIs = memberEPAService.ConvertKPIInfoToXML(model.KPIInfo),
                MarkedBy = this.CurrentUser.UserId,
                MarkedDate = DateTime.Now
            };

            CommandResult result = memberEPAService.AddOrUpdateEmployeeEPA(criteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get Current User's EPA.
        /// </summary>
        /// <param name="targetId">Target id.</param>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageMyEPA, Policy = Policy.Read)]
        public HttpResponseMessage GetMyEPA(int targetId, int employeeId)
        {
            int currentUserId = this.CurrentUser.UserId;
            EPAInfoModel epaInfo = null;
            if (currentUserId == employeeId)
            {
                epaInfo = GetEPAInformation(targetId, employeeId);
                return Request.CreateResponse(HttpStatusCode.OK, epaInfo);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, epaInfo);
            }
        }

        /// <summary>
        /// Method Get Employee EPA.
        /// </summary>
        /// <param name="targetId">Target Id.</param>
        /// <param name="employeeId">Employee Id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageMemberEPA, Policy = Policy.Read)]
        public HttpResponseMessage GetEmployeeEPA(int targetId, int employeeId)
        {
            EPAInfoModel epaInfo = null;
            var checkPermissionCriteria = new GetEmployeePermissionCriteria()
            {
                ActorUserId = this.CurrentUser.UserId,
                EmployeeId = employeeId,
                TargetId = targetId
            };
            int permission = memberEPAService.CheckEmployeePermission(checkPermissionCriteria);

            switch (permission)
            {
                case 0:
                    return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
                default:
                    epaInfo = GetEPAInformation(targetId, employeeId);
                    if (epaInfo != null)
                    {
                        epaInfo.Permission = permission;
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, epaInfo);
            }
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageMyEPA, Policy = Policy.Read)]
        public List<MemberEPAModel> GetEPAOfCurrentMember(int? projectId = null, int? status = null)
        {
            var criteria = new GetEPAByEmployeeCriteria
            {
                EmployeeId = this.CurrentUser.UserId,
                ProjectId = projectId,
                Status = status
            };

            return memberEPAService.GetEPAByEmployee(criteria);
        }

        /// <summary>
        /// Get EPA of employees by manager.
        /// </summary>
        /// <param name="fromDate">The from date.</param>
        /// <param name="toDate">The to date.</param>
        /// <param name="teamId">The team id.</param>
        /// <param name="targetId">The target id.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="status">The status.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="isShowAll">Show all EPA.</param>
        /// <returns>The list Member EPA Model.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageMemberEPA, Policy = Policy.Read)]
        [HttpGet]
        public List<GroupEPAOfMemberModel> GetEPAOfEmployeeByManager(
            DateTime fromDate,
            DateTime? toDate,
            int? teamId = null,
            int? targetId = null,
            int? projectId = null,
            int? status = null,
            string employeeId = null,
            bool? isShowAll = null)
        {
            var criteria = new GetEPAOfAllEmployeeByManagerCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                FromDate = fromDate,
                ToDate = toDate,
                TeamId = teamId,
                EmployeeId = employeeId,
                TargetId = targetId,
                ProjectId = projectId,
                Status = status,
                IsShowAll = isShowAll
            };

            return memberEPAService.GetEPAOfEmployeeByManager(criteria);
        }

        /// <summary>
        /// Update EPA Status.
        /// </summary>
        /// <param name="model">EPA Information Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageMemberEPA, Policy = Policy.Host)]
        public HttpResponseMessage UpdateEPAStatus(EPAInfoModel model)
        {
            var updateEPAStatusCriteria = new UpdateEPAStatusCriteria()
            {
                EmployeeId = model.EmployeeId,
                Status = model.Status,
                TargetId = model.TargetId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = memberEPAService.UpdateEPAStatus(updateEPAStatusCriteria);
            if (result.Code != 0)
            {
                var warningMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.OK, warningMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetPermission()
        {
            int policy = commonService.GetPermission(EPAFunctionKeys.ManageMemberEPA);
            return policy;
        }

        /// <summary>
        /// Get appraisal form.
        /// </summary>
        /// <param name="year">The EPA year.</param>
        /// <param name="month">The EPA month.</param>
        /// <param name="epaTypeId">The EPA type Id.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns>The enumerator Appraisal Form.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageAppraisalForm, Policy = Policy.Read)]
        public IEnumerable<AppraisalFormModel> GetAppraisalForm(
            int year,
            int month,
            int? epaTypeId = null,
            string keyword = "")
        {
            var criteria = new GetAppraisalFormCriteria
            {
                EPATypeId = epaTypeId,
                Month = month,
                Year = year,
                Keyword = keyword
            };

            return memberEPAService.GetAppraisalForm(criteria);
        }

        /// <summary>
        /// Insert appraisal form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageAppraisalForm, Policy = Policy.Host)]
        public CommandResult InsertAppraisalForm(InsertAppraisalFormCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
            }

            return memberEPAService.InsertAppraisalForm(criteria);
        }

        [HttpGet]
        public IEnumerable<ProcessPeriodModel> GetAllProcessPeriod()
        {
            var criteria = new GetAllProcessPeriodCriteria();

            return memberEPAService.GetAllProcessPeriod(criteria);
        }

        /// <summary>
        /// Get Appraisal Form by Id.
        /// </summary>
        /// <param name="epaId">The EPA Id.</param>
        /// <returns>The Appraisal Form model.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageAppraisalForm, Policy = Policy.Read)]
        public AppraisalFormModel GetAppraisalFormById(int epaId)
        {
            var criteria = new GetAppraisalFormByIdCriteria
            {
                EPAId = epaId
            };

            return memberEPAService.GetAppraisalFormById(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageAppraisalForm, Policy = Policy.Host)]
        public CommandResult UpdateAppraisalForm(UpdateAppraisalFormCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return memberEPAService.UpdateAppraisalForm(criteria);
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageAppraisalForm, Policy = Policy.Host)]
        public CommandResult DeleteAppraisalForm(int epaId)
        {
            var criteria = new DeleteAppraisalFormCriteria
            {
                EPAId = epaId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            return memberEPAService.DeleteAppraisalForm(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageAppraisalForm, Policy = Policy.Host)]
        public CommandResult UpdateStatusAppraisalForm(UpdateStatusAppraisalFormCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return memberEPAService.UpdateStatusAppraisalForm(criteria);
        }

        [HttpGet]
        public int GetAppraisalFormPermission()
        {
            int policy = commonService.GetPermission(EPAFunctionKeys.ManageAppraisalForm);
            return policy;
        }

        [HttpGet]
        public int GetUserPermissonOnTarget(int targetId)
        {
            var criteria = new GetUserPermissionOnTargetCriteria
            {
                TargetId = targetId,
                ActorUserId = this.CurrentUser.UserId
            };

            return memberEPAService.GetUserPermissionOnTarget(criteria);
        }

        [HttpGet]
        public bool GetIsShowFullEPAComment()
        {
            string isShowFullEPAComment = System.Configuration.ConfigurationManager.AppSettings[_ShowFullEPACommentKey];

            if (!string.IsNullOrEmpty(isShowFullEPAComment))
            {
                return isShowFullEPAComment.Equals("true") ? true : false;
            }

            return false;
        }

        /// <summary>
        /// Method Get EPAInfo.
        /// </summary>
        /// <param name="targetId">Target Id.</param>
        /// <param name="employeeId">Employee Id.</param>
        /// <returns>EPAInfo Model.</returns>
        private EPAInfoModel GetEPAInformation(int targetId, int employeeId)
        {
            EPAInfoModel data = null;
            var getEPAInfoCriteria = new GetEPAInfoCriteria()
            {
                EmployeeId = employeeId,
                TargetId = targetId
            };

            data = memberEPAService.GetEPAInfo(getEPAInfoCriteria);

            return data;
        }

        [HttpPut]
        public HttpResponseMessage UpdateCommentEPA(EPAInfoKPIModel model)
        {
            var criteria = new UpdateEPACommentsCriteria()
            {
                Comments = memberEPAService.ConvertEPACommentsToXML(model.Employees)
            };
            CommandResult result = memberEPAService.UpdateCommentEPA(criteria);

            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}