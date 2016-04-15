namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Services.Implement;
    using NexEPA.MemberEPA.Services.Interface;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Services.Implement;
    using NexEPA.Project.Services.Interface;
    using NexEPA.Target.Criteria.Target;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models;
    using NexEPA.Target.Services.Implement;
    using NexEPA.Target.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// Target Controller.
    /// </summary>
    public class TargetController : BaseController
    {
        private ITargetService targetService;

        private IProjectService projectService;

        private ICommonService commonService;

        private IMemberEPAService memberEPAService;

        public TargetController()
            : this(null, null, null, null)
        {
        }

        public TargetController(
            ITargetService targetService,
            IProjectService projectService,
            ICommonService commonService,
            IMemberEPAService memberEPAService)
        {
            this.targetService = targetService ?? new TargetService();
            this.projectService = projectService ?? new ProjectService();
            this.commonService = commonService ?? new CommonService();
            this.memberEPAService = memberEPAService ?? new MemberEPAService();
        }

        /// <summary>
        /// Method Get Target.
        /// </summary>
        /// <param name="targetTypeId">Target Type Id.</param>
        /// <param name="startDate">Query Start date.</param>
        /// <param name="endDate">Query End Date.</param>
        /// <returns>The Target Model.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<TargetModel> GetTarget(
            int? targetTypeId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var criteria = new GetTargetCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                TargetTypeId = targetTypeId,
                StartDate = startDate,
                EndDate = endDate.HasValue ? endDate.Value.AddDays(1) : (DateTime?)null
            };

            return targetService.GetAllTarget(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public TargetModel GetTargetById(int targetId)
        {
            var criteria = new GetTargetByIdCriteria
            {
                TargetId = targetId,
                ActorUserId = this.CurrentUser.UserId
            };

            return targetService.GetTargetById(criteria);
        }

        /// <summary>
        /// Insert new Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult InsertTarget(InsertTargetCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
                //// default status is Open when create the new target
                criteria.Status = (int)Status.Open;
                //// default target type is Project
                criteria.TargetTypeId = 2;
            }

            return targetService.Insert(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult Put(UpdateTargetCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return targetService.Update(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return targetService.UpdateStatus(criteria);
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult Delete(int targetId)
        {
            var criteria = new DeleteTargetCriteria
            {
                TargetId = targetId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            return targetService.Delete(criteria);
        }

        [HttpGet]
        public IEnumerable<dynamic> GetMilestoneByProjectId(int projectId)
        {
            var getMilestoneCriteria = new GetMilestoneByProjectIdCriteria()
            {
                ProjectId = projectId
            };
            var milestoneList = projectService.GetMilestoneByProjectId(getMilestoneCriteria);

            IEnumerable<dynamic> milestoneResult = milestoneList.Select(mi => new { mi.Name, mi.MilestoneId });
            return milestoneResult;
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<EmployeeModel> GetEmployeeByTargetId(int targetId)
        {
            var criteria = new GetAllEmployeeByTargetIdCriteria
            {
                TargetId = targetId
            };

            return targetService.GetTargetEmployeeByTargetId(criteria);
        }

        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.AddedBy = this.CurrentUser.UserId;
                criteria.AddedDate = DateTime.Now;
            }

            return targetService.InsertTargetEmployee(criteria);
        }

        /// <summary>
        /// Delete Target Employee.
        /// </summary>
        /// <param name="targetId">The target ID.</param>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>The Command Result.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult DeleteTargetEmployee(int targetId, int employeeId)
        {
            var criteria = new DeleteTargetEmployeeCriteria
            {
                TargetId = targetId,
                EmployeeId = employeeId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            return targetService.DeleteTargetEmployee(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public List<TargetKPIGroupModel> GetKPIByTargetId(int targetId, int? groupId = null)
        {
            var criteria = new GetKPIByTargetIdCriteria
            {
                TargetId = targetId,
                KPIGroupId = groupId
            };

            return targetService.GetKPIByTargetId(criteria);
        }

        [HttpGet]
        public TargetDateModel GetProjectOrMilestoneDateById(int projectId, int? milestoneId = null)
        {
            var criteria = new GetProjectOrMilestoneDateCriteria
            {
                ProjectId = projectId,
                MilestoneId = milestoneId
            };

            return targetService.GetProjectOrMilestoneDate(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<dynamic> GetKPIGroupByTargetId(int targetId)
        {
            var criteria = new GetKPIGroupByTargetIdCriteria
            {
                TargetId = targetId
            };

            return targetService.GetKPIGroupByTargetId(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<TargetFilterModel> GetTargetToFilter()
        {
            var criteria = new GetTargetToFilterCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
            };

            return targetService.GetTargetToFilter(criteria);
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetPermission()
        {
            int policy = commonService.GetPermission(EPAFunctionKeys.ManageTarget);
            return policy;
        }

        [HttpGet]
        public int GetSyncTargetPermission()
        {
            int policy = commonService.GetPermission(EPAFunctionKeys.SyncRedmineTarget);

            return policy;
        }

        /// <summary>
        /// Get employee by KPI.
        /// </summary>
        /// <param name="targetId">The target ID.</param>
        /// <param name="kpiGroupId">The KPI group ID.</param>
        /// <param name="kpiId">The KPI ID.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByKPI(
            int targetId,
            int kpiGroupId,
            int kpiId)
        {
            var criteria = new GetEmployeeByKPICriteria
            {
                TargetId = targetId,
                KPIGroupId = kpiGroupId,
                KPIId = kpiId
            };

            return targetService.GetEmployeeByKPI(criteria);
        }

        /// <summary>
        /// Update KPI score.
        /// </summary>
        /// <param name="epaInfo">The EPA info.</param>
        /// <returns>The command result.</returns>
        [HttpPut]
        public CommandResult UpdateKPIScore(EPAInfoKPIModel epaInfo)
        {
            var criteria = new UpdateKPIScoreCriteria
            {
                TargetId = epaInfo.TargetId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now,
                KPIId = epaInfo.KPIId,
                KPIGroupId = epaInfo.KPIGroupId,
                EmployeeScore = targetService.ConvertTargetKPIInfoToXML(epaInfo.Employees, epaInfo.KPIGroupId, epaInfo.KPIId)
            };

            return memberEPAService.UpdateKPIScore(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.SyncRedmineTarget, Policy = Policy.Read)]
        public IList<ProjectTargetModel> GetRedmineProjectTarget(
            DateTime fromDate,
            DateTime toDate,
            string projectIds)
        {
            var criteria = new GetRedmineProjectTargetCriteria
            {
                FromDate = fromDate,
                ToDate = toDate.AddDays(1),
                ProjectIds = projectIds
            };

            IList<ProjectTargetModel> projectTargets = targetService.GetRedmineProjectTarget(criteria);

            return projectTargets;
        }

        [HttpGet]
        public RedmineIssueModel GetRedmineFeatureInfo(int redmineFeatureId)
        {
            var criteria = new GetRedmineFeatureInfoCriteria
            {
                RedmineFeatureId = redmineFeatureId
            };

            RedmineIssueModel feature = targetService.GetRedmineFeatureInfo(criteria);

            return feature;
        }

        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.SyncRedmineTarget, Policy = Policy.Host)]
        public CommandResult SyncTargetFromRedmineFeature(IList<CommonTargetModel> targets)
        {
            var criteria = new SyncTargetFromRedmineFeatureCriteria
            {
                AddedBy = CurrentUser.UserId,
                Targets = targets
            };

            CommandResult result = targetService.SyncTargetFromRedmineFeature(criteria);

            return result;
        }
    }
}