// -----------------------------------------------------------------------
// <copyright file="TargetService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Implement
{
    using System.Collections;
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
    using NexEPA.Target.Criteria.Target;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models;
    using NexEPA.Target.Repositories.Implement;
    using NexEPA.Target.Repositories.Interface;
    using NexEPA.Target.Services.Interface;

    /// <summary>
    /// The implement of Target Service.
    /// </summary>
    public class TargetService : BaseService, ITargetService
    {
        private ITargetRepository _targetRepository;
        private IEncodeService _encodeService;

        public TargetService(ITargetRepository targetRepository = null, IEncodeService encodeService = null)
        {
            _targetRepository = targetRepository ?? new TargetRepository();
            _encodeService = encodeService ?? new EncodeService();
        }

        /// <summary>
        /// Get target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Target Model.</returns>
        public IEnumerable<TargetModel> GetAllTarget(GetTargetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<TargetModel> rawTargets = null;

            if (criteria.IsValid())
            {
                rawTargets = _targetRepository.GetTarget(criteria);

                foreach (TargetModel rawTarget in rawTargets)
                {
                    rawTarget.StatusName = EnumStatus.GetDescripton((Status)rawTarget.Status);
                }
            }

            return rawTargets;
        }

        /// <summary>
        /// Get Target by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Target Model.</returns>
        public TargetModel GetTargetById(GetTargetByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var targetItem = new TargetModel();

            if (criteria.IsValid())
            {
                targetItem = _targetRepository.GetTargetById(criteria);

                if (targetItem != null)
                {
                    targetItem.StatusName = EnumStatus.GetDescripton((Status)targetItem.Status);
                }
            }

            return targetItem;
        }

        /// <summary>
        /// Insert new target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Insert(InsertTargetCriteria criteria)
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
        /// Update Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Update(UpdateTargetCriteria criteria)
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
        /// Update Target Status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria)
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
        /// Delete Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Delete(DeleteTargetCriteria criteria)
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
        /// Get Target's Employee by Target Id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetTargetEmployeeByTargetId(GetAllEmployeeByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employeeList = null;

            if (criteria.IsValid())
            {
                employeeList = _targetRepository.GetEmployee(criteria);
            }

            return employeeList;
        }

        /// <summary>
        /// Insert Target's Employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria)
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
        /// Delete Target's Employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult DeleteTargetEmployee(DeleteTargetEmployeeCriteria criteria)
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
        /// Get KPIs by Target ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List KPI Group Model.</returns>
        public List<TargetKPIGroupModel> GetKPIByTargetId(GetKPIByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiGroupItemList = new List<TargetKPIGroupModel>();

            if (criteria.IsValid())
            {
                var rawKPIsInfo = (List<KPIModel>)_targetRepository.GetKPIByTargetId(criteria);

                if (rawKPIsInfo != null && rawKPIsInfo.Count > 0)
                {
                    for (int i = 0; i < rawKPIsInfo.Count; i++)
                    {
                        KPIModel rawKPIInfo = rawKPIsInfo[i];

                        var kpiGroupItem = new TargetKPIGroupModel
                        {
                            Group = rawKPIInfo.Group
                        };

                        kpiGroupItem.KPIs.Add(rawKPIInfo);

                        List<KPIModel> kpiSameGroup = rawKPIsInfo.Where(item => item.Group.KPIGroupId == rawKPIInfo.Group.KPIGroupId
                                                && item.KPIId != rawKPIInfo.KPIId).ToList();

                        foreach (KPIModel kpiInfo in kpiSameGroup)
                        {
                            kpiGroupItem.KPIs.Add(kpiInfo);
                            rawKPIsInfo.Remove(kpiInfo);
                        }

                        kpiGroupItemList.Add(kpiGroupItem);
                    }
                }
            }

            return kpiGroupItemList;
        }

        /// <summary>
        /// Get project date or milestone date.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Target Date Model.</returns>
        public TargetDateModel GetProjectOrMilestoneDate(GetProjectOrMilestoneDateCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var targetDate = new TargetDateModel();

            if (criteria.IsValid())
            {
                targetDate = GetData<TargetDateModel>(criteria).FirstOrDefault();
            }

            return targetDate;
        }

        /// <summary>
        /// Get KPI Group by target id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator dynamic KPI Group include KPIGroupId and Name.</returns>
        public IEnumerable<dynamic> GetKPIGroupByTargetId(GetKPIGroupByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<dynamic> groups = null;

            if (criteria.IsValid())
            {
                IEnumerable<KPIGroupModel> rawGroups = GetData<KPIGroupModel>(criteria);
                groups = rawGroups.Select(gr => new { gr.KPIGroupId, gr.Name });
            }

            return groups;
        }

        /// <summary>
        /// Get Target to filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Target Filter Model.</returns>
        public IEnumerable<TargetFilterModel> GetTargetToFilter(GetTargetToFilterCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<TargetFilterModel> targets = null;

            if (criteria.IsValid())
            {
                targets = GetData<TargetFilterModel>(criteria);
            }

            return targets;
        }

        /// <summary>
        /// Get employee by KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployeeByKPI(GetEmployeeByKPICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employees = null;

            if (criteria.IsValid())
            {
                TemporaryEPAAndEmployeeModel temporaryEPA = _targetRepository.GetEmployeeByKPI(criteria);

                employees = GetEPAInformationOfEmployee(temporaryEPA.Employees, temporaryEPA.RawEPAs);
            }

            return employees;
        }

        public IList<ProjectTargetModel> GetRedmineProjectTarget(GetRedmineProjectTargetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IList<ProjectTargetModel> projectTargets = null;

            if (criteria.IsValid())
            {
                projectTargets = _targetRepository.GetRedmineProjectTarget(criteria);
            }

            return projectTargets;
        }

        /// <summary>
        /// Convert KPI info to XMl before update.
        /// </summary>
        /// <param name="employees">The list employee.</param>
        /// <param name="kpiGroupId">The KPI group Id.</param>
        /// <param name="kpiId">THe KPI Id.</param>
        /// <returns>The XML contains information about KPI.</returns>
        public string ConvertTargetKPIInfoToXML(IEnumerable<EmployeeModel> employees, int kpiGroupId, int kpiId)
        {
            StringBuilder epaPattern = new StringBuilder(Constants.BeginRootTag);

            foreach (EmployeeModel employee in employees)
            {
                foreach (CoreEPAModel epa in employee.EPAInfo)
                {
                    if (epa.KPIGroupId == kpiGroupId)
                    {
                        foreach (KPIModel kpiItem in epa.KPIItems)
                        {
                            if (kpiItem.KPIId == kpiId)
                            {
                                string cipherScore = !string.IsNullOrEmpty(kpiItem.Score) ? _encodeService.Encode(kpiItem.Score + "_" + kpiId) : null;
                                string cipherAverageScore = _encodeService.Encode(employee.FinalScore);
                                epaPattern.AppendFormat(Constants.TargetEPAScore, employee.EmployeeId, cipherScore, HttpUtility.HtmlEncode(kpiItem.Comment), cipherAverageScore);
                            }
                        }
                    }
                }
            }

            epaPattern.Append(Constants.EndRootTag);
            return epaPattern.ToString();
        }

        public RedmineIssueModel GetRedmineFeatureInfo(GetRedmineFeatureInfoCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            RedmineIssueModel feature = null;

            if (criteria.IsValid())
            {
                feature = GetDataEntity<RedmineIssueModel>(criteria);
            }

            return feature;
        }

        private IEnumerable<EmployeeModel> GetEPAInformationOfEmployee(
            IEnumerable<EmployeeModel> employees,
            IEnumerable<TemporaryEPAModel> kpiItems)
        {
            foreach (EmployeeModel employee in employees)
            {
                IEnumerable<TemporaryEPAModel> kpiItemsOfEmployee = kpiItems.Where(item =>
                    item.EmployeeId == employee.EmployeeId);
                employee.EPAInfo = GetEPAInformationOfEmployeeDetails(kpiItemsOfEmployee);
            }

            return employees;
        }

        /// <summary>
        /// Get EPA information of employee in details.
        /// </summary>
        /// <param name="kpiItems">The enumerator KPI information.</param>
        /// <returns>The list Core EPA Model.</returns>
        private List<CoreEPAModel> GetEPAInformationOfEmployeeDetails(IEnumerable<TemporaryEPAModel> kpiItems)
        {
            var coreEPAInfoList = new List<CoreEPAModel>();
            IEnumerable<int> kpiGroupIds = kpiItems.Select(item => item.KPIGroupId).Distinct();

            foreach (int kpiGroupId in kpiGroupIds)
            {
                IEnumerable<TemporaryEPAModel> kpiItemsOfGroup = kpiItems.Where(item => item.KPIGroupId == kpiGroupId);

                if (kpiItemsOfGroup != null && kpiItemsOfGroup.Count() > 0)
                {
                    var epaInfo = new CoreEPAModel
                    {
                        KPIGroupId = kpiGroupId,
                        IsSpreadKPI = kpiItemsOfGroup.First().IsSpreadKPI,
                        KPIGroupWeight = kpiItemsOfGroup.First().KPIGroupWeight,
                        Status = kpiItemsOfGroup.First().Status
                    };

                    foreach (TemporaryEPAModel rawKPIItem in kpiItemsOfGroup)
                    {
                        string kpiScore = !string.IsNullOrEmpty(rawKPIItem.Score)
                            ? _encodeService.Decode(rawKPIItem.Score).Replace("_" + rawKPIItem.KPIId, string.Empty)
                            : string.Empty;
                        var kpiItem = new KPIModel
                        {
                            KPIId = rawKPIItem.KPIId,
                            Score = kpiScore,
                            Weight = rawKPIItem.Weight,
                            Comment = rawKPIItem.Comment,
                            Percent = !string.IsNullOrEmpty(kpiScore) ? int.Parse(kpiScore) * 10 : 0
                        };

                        epaInfo.KPIItems.Add(kpiItem);
                    }

                    coreEPAInfoList.Add(epaInfo);
                }
            }

            return coreEPAInfoList;
        }
    }
}
