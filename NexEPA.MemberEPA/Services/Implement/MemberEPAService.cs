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
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Services;
    using NexEPA.Core.Utilities;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
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

        public MemberEPAService(IMemberEPARepository memberEPARepository = null)
        {
            _memberEPARepository = memberEPARepository ?? new MemberEPARepository();
        }

        public EPAInfoModel GetEPAInfo(GetEPAInfoCriteria criteria)
        {
            EPAInfoModel result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            { 
                result = _memberEPARepository.GetEPAInfo(criteria);
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
                    employeeEPA.StatusName = EnumStatus.GetDescripton((Status)employeeEPA.Status);
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
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get EPA of employee by manager.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list MemberEPAModel.</returns>
        public List<MemberEPAModel> GetEPAOfEmployeeByManager(GetEPAOfAllEmployeeByManagerCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employees = new List<MemberEPAModel>();

            if (criteria.IsValid())
            {
                employees = (List<MemberEPAModel>)_memberEPARepository.GetEPAOfEmployeeByManager(criteria);

                foreach (var employee in employees)
                {
                    employee.StatusName = EnumStatus.GetDescripton((Status)employee.Status);
                }
            }

            return employees;
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
                result = this._Repository.Update(criteria);
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
                commandResult = _Repository.Insert(criteria);
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
                commandResult = _Repository.Update(criteria);
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
                commandResult = _Repository.Delete(criteria);
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
                commandResult = _Repository.Update(criteria);
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
                commandResult = _Repository.Update(criteria);
            }

            return commandResult;
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
    }
}
