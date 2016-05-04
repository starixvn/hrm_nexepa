// --------------------------------------------------------------------------
// <copyright file="MemberEPAService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
//   <change who="Josh.Phan" date="2014.4.22">Added - GetEPAByEmployee</change>
// </history>

namespace NexEPA.MemberEPA.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Fanex.Common.Helper;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Core.Utilities;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MemberEPA;
    using NexEPA.MemberEPA.Models.MyEPA;
    using NexEPA.MemberEPA.Repositories.Implement;
    using NexEPA.MemberEPA.Repositories.Interface;
    using NexEPA.MemberEPA.Services.Interface;

    /// <summary>
    /// Member EPA Service.
    /// </summary>
    public class MemberEPAService : BaseService, IMemberEPAService
    {
        private readonly IMemberEPARepository _memberEPARepository;

        private readonly IEncodeService _encodeService;

        public MemberEPAService()
            : this(null, null)
        {
        }

        public MemberEPAService(IMemberEPARepository memberEPARepository, IEncodeService encodeService)
        {
            this._memberEPARepository = memberEPARepository ?? new MemberEPARepository();
            this._encodeService = encodeService ?? new EncodeService();
        }

        /// <summary>
        /// Get EPA information.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The EPA info model.</returns>
        public EPAInfoModel GetEPAInfo(GetEPAInfoCriteria criteria)
        {
            EPAInfoModel result = null;
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                result = _memberEPARepository.GetEPAInfo(criteria);

                if (result != null)
                {
                    string finalScore = !string.IsNullOrEmpty(result.FinalScore) ? Decode(result.FinalScore) : string.Empty;
                    finalScore = CommonHelper.PercentToStarNumber(finalScore);

                    result.KPIInfo = CalculatePercentOfKPI(result.KPIInfo);
                    result.FinalScore = finalScore;
                }
            }

            return result;
        }

        /// <summary>
        /// Get EPA by employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List MemberEPAModel.</returns>
        public List<MemberEPAModel> GetEPAByEmployee(GetEPAByEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeeEPAs = new List<MemberEPAModel>();

            if (criteria.IsValid())
            {
                employeeEPAs = (List<MemberEPAModel>)_memberEPARepository.GetEPAByEmployee(criteria);

                foreach (var employeeEPA in employeeEPAs)
                {
                    string score = !string.IsNullOrEmpty(employeeEPA.Score) ? Decode(employeeEPA.Score) : string.Empty;

                    employeeEPA.StatusName = EnumStatus.GetDescripton((Status)employeeEPA.Status);
                    employeeEPA.Score = CommonHelper.PercentToStarNumber(score);
                }
            }

            return employeeEPAs;
        }

        /// <summary>
        /// Add or Update Employee EPA.
        /// </summary>
        /// <param name="criteria">AddOrUpdateEPAScore Criteria.</param>
        /// <returns>Command Result.</returns>
        public CommandResult AddOrUpdateEmployeeEPA(AddOrUpdateEPAScoreCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Insert(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get EPA of employee by manager.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list MemberEPAModel.</returns>
        public List<GroupEPAOfMemberModel> GetEPAOfEmployeeByManager(GetEPAOfAllEmployeeByManagerCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employees = new List<MemberEPAModel>();

            if (criteria.IsValid())
            {
                employees = (List<MemberEPAModel>)_memberEPARepository.GetEPAOfEmployeeByManager(criteria);

                foreach (var employee in employees)
                {
                    string score = (string.IsNullOrEmpty(employee.Score) || employee.Score.Equals("0")) ? "0" : Decode(employee.Score);

                    employee.StatusName = EnumStatus.GetDescripton((Status)employee.Status);
                    employee.Score = CommonHelper.PercentToStarNumber(score);
                }
            }

            var groupEmployees = ProcessEPAOfEmployeeByManager(employees);

            foreach (var employee in groupEmployees)
            {
                employee.EPACount = employee.EPAs.Count;

                int count = 0;
                decimal totalScore = 0;

                foreach (var item in employee.EPAs)
                {
                    decimal score = CommonHelper.ConvertDecimal(item.Score);

                    if (score > 0)
                    {
                        totalScore += score;
                        count++;
                    }
                }

                employee.AverageScore = count > 0 ? (totalScore / count) : 0;
            }

            return groupEmployees;
        }

        /// <summary>
        /// Update EPA Status.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>Command Result.</returns>
        public CommandResult UpdateEPAStatus(UpdateEPAStatusCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this.Repository.Update(criteria);
            }

            return result;
        }

        /// <summary>
        /// Check employee permission.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>A value to check permission.</returns>
        public int CheckEmployeePermission(GetEmployeePermissionCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            int result = -1;
            if (criteria.IsValid())
            {
                result = GetDataEntity<int>(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get appraisal form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Appraisal Form.</returns>
        public IEnumerable<AppraisalFormModel> GetAppraisalForm(GetAppraisalFormCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<AppraisalFormModel> appraisalList = null;

            if (criteria.IsValid())
            {
                appraisalList = _memberEPARepository.GetAppraisalForm(criteria);

                foreach (AppraisalFormModel rawAppraisalFormItem in appraisalList)
                {
                    rawAppraisalFormItem.StatusName = EnumStatus.GetDescripton((Status)rawAppraisalFormItem.Status);
                }
            }

            return appraisalList;
        }

        /// <summary>
        /// Insert appraisal form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult InsertAppraisalForm(InsertAppraisalFormCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Insert(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Get all process period.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Process Period Model.</returns>
        public IEnumerable<ProcessPeriodModel> GetAllProcessPeriod(GetAllProcessPeriodCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<ProcessPeriodModel> processPeriodList = null;

            if (criteria.IsValid())
            {
                processPeriodList = GetData<ProcessPeriodModel>(criteria);
            }

            return processPeriodList;
        }

        /// <summary>
        /// Get Appraisal Form by Id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Appraisal From model.</returns>
        public AppraisalFormModel GetAppraisalFormById(GetAppraisalFormByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            AppraisalFormModel appraisal = new AppraisalFormModel();

            if (criteria.IsValid())
            {
                appraisal = _memberEPARepository.GetAppraisalFormById(criteria);

                if (appraisal != null)
                {
                    appraisal.StatusName = EnumStatus.GetDescripton((Status)appraisal.Status);
                    appraisal.EPAProcess = GroupEPAProcess(appraisal.EPAProcess, appraisal.Employee.Code);
                }
            }

            return appraisal;
        }

        /// <summary>
        /// Update Appraisal Form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateAppraisalForm(UpdateAppraisalFormCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Delete Appraisal Form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult DeleteAppraisalForm(DeleteAppraisalFormCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Delete(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Update status of Appraisal Form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateStatusAppraisalForm(UpdateStatusAppraisalFormCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Update KPI score.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateKPIScore(UpdateKPIScoreCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Get user permission on target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The permission.</returns>
        public int GetUserPermissionOnTarget(GetUserPermissionOnTargetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            int permission = 0;

            if (criteria.IsValid())
            {
                permission = GetData<int>(criteria).FirstOrDefault();
            }

            return permission;
        }

        public string Encode(string plainText)
        {
            return _encodeService.Encode(plainText);
        }

        public string Decode(string cipherText)
        {
            return _encodeService.Decode(cipherText);
        }

        /// <summary>
        /// Convert KPI info to XMl.
        /// </summary>
        /// <param name="kpiInfo">The KPI info.</param>
        /// <returns>The EPA xml.</returns>
        public string ConvertKPIInfoToXML(List<GroupingKPIModel> kpiInfo)
        {
            string epaXML = string.Empty;

            if (kpiInfo != null)
            {
                var value = new StringBuilder(Constants.BeginRootTag);

                foreach (GroupingKPIModel kpi in kpiInfo)
                {
                    foreach (var item in kpi.KPIGroupItems)
                    {
                        string cipherScore = !string.IsNullOrEmpty(item.Score) ? Encode(item.Score + "_" + item.KPIId) : null;

                        value.AppendFormat(Constants.EmployeeEPAScore, item.KPIId, item.Group.KPIGroupId, cipherScore, HttpUtility.HtmlEncode(item.Comment));
                    }
                }

                epaXML = value.Append(Constants.EndRootTag).ToString();
            }

            return epaXML;
        }

        /// <summary>
        /// Group EPA Process.
        /// </summary>
        /// <param name="epaProcess">The EPA process list.</param>
        /// <param name="selfCode">The code of employee.</param>
        /// <returns>The EPA process list after group process.</returns>
        private IEnumerable<EPAProcessModel> GroupEPAProcess(
            IEnumerable<EPAProcessModel> epaProcess,
            string selfCode)
        {
            List<EPAProcessModel> epaProcessList = epaProcess.ToList();

            for (int i = 0; i < epaProcessList.Count(); i++)
            {
                EPAProcessModel process = epaProcessList[i];

                if (process.Evaluator.Code == null)
                {
                    process.Evaluator.Code = selfCode;
                }

                process.EvaluatorList.Add(process.Evaluator);

                List<EPAProcessModel> rawSameProcess = epaProcessList.Where(pro => pro.ProcessId == process.ProcessId &&
                                                        pro.Evaluator.EmployeeId != process.Evaluator.EmployeeId).ToList();

                for (int j = 0; j < rawSameProcess.Count(); j++)
                {
                    EPAProcessModel sameProcess = rawSameProcess[j];
                    process.EvaluatorList.Add(sameProcess.Evaluator);
                    epaProcessList.Remove(sameProcess);
                }

                process.Evaluator = null;
            }

            return epaProcessList;
        }

        /// <summary>
        /// Calculate percent of each KPI.
        /// </summary>
        /// <param name="allGroupKPIs">All group KPI.</param>
        /// <returns>All group KPI has percent.</returns>
        private List<GroupingKPIModel> CalculatePercentOfKPI(List<GroupingKPIModel> allGroupKPIs)
        {
            foreach (GroupingKPIModel group in allGroupKPIs)
            {
                List<KPIModel> kpiOfGroup = group.KPIGroupItems;

                foreach (KPIModel kpiItem in kpiOfGroup)
                {
                    string plainScore = !string.IsNullOrEmpty(kpiItem.Score) ? Decode(kpiItem.Score).Replace("_" + kpiItem.KPIId, string.Empty) : string.Empty;

                    kpiItem.Percent = CommonHelper.ConvertInt(plainScore);
                    kpiItem.Score = plainScore;
                }
            }

            return allGroupKPIs;
        }

        /// <summary>
        /// Process EPA of employee.
        /// </summary>
        /// <param name="memberEPAs">The list EAP.</param>
        /// <returns>All EPA group by employee.</returns>
        private List<GroupEPAOfMemberModel> ProcessEPAOfEmployeeByManager(List<MemberEPAModel> memberEPAs)
        {
            var groupMemberEPAs = new List<GroupEPAOfMemberModel>();
            var groupEmployIDs = new List<int>();

            if (memberEPAs != null)
            {
                foreach (var member in memberEPAs)
                {
                    var groupMemberEPA = new GroupEPAOfMemberModel();

                    if (!groupEmployIDs.Contains(member.EmployeeId))
                    {
                        groupMemberEPA.EmployeeId = member.EmployeeId;
                        groupMemberEPA.NickName = member.NickName;
                        groupMemberEPA.FullName = member.FullName;

                        groupMemberEPA.EPAs.Add(member);

                        groupMemberEPAs.Add(groupMemberEPA);
                        groupEmployIDs.Add(member.EmployeeId);
                    }
                    else
                    {
                        var emp = groupMemberEPAs.Where(p => p.EmployeeId == member.EmployeeId).SingleOrDefault();
                        emp.EPAs.Add(member);
                    }
                }
            }

            return groupMemberEPAs;
        }

        public CommandResult UpdateCommentEPA(UpdateEPACommentsCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }

        public string ConvertEPACommentsToXML(IEnumerable<EmployeeModel> employees)
        {
            StringBuilder epaPattern = new StringBuilder(Constants.BeginRootTag);

            foreach (EmployeeModel employee in employees)
            {
                string encodedComment = HttpUtility.HtmlEncode(employee.EPA.Comment);

                epaPattern.AppendFormat(Constants.EPAComments, employee.EPA.EPAId, encodedComment);
            }

            epaPattern.Append(Constants.EndRootTag);
            return epaPattern.ToString();
        }
    }
}