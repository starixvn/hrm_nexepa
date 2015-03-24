// --------------------------------------------------------------------------
// <copyright file="ResourcePlanningReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Employee.Models;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.ResourcePlanningReport;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// ResourcePlanningReportService class.
    /// </summary>
    public class ResourcePlanningReportService : BaseService, IResourcePlanningReportService
    {
        private IResourcePlanningReportRepository _repository;

        public ResourcePlanningReportService()
            : this(null)
        {
        }

        public ResourcePlanningReportService(IResourcePlanningReportRepository repository)
        {
            _repository = repository ?? new ResourcePlanningReportRepository();
        }

        /// <summary>
        /// Gets the redmine resource planning.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The redmine resource planning.</returns>
        public RedmineResourcePlanningViewModel GetRedmineResourcePlanning(GetRedmineResourcePlanningCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var reportData = new RedmineResourcePlanningViewModel();

            if (criteria.IsValid())
            {
                reportData = _repository.GetRedmineResourcePlanning(criteria);
                reportData.ProjectResources = GetDistinctProjectResources(reportData.ProjectResources);
                reportData.MonthDateInfo = GetDistinctMonthDateInfo(reportData.MonthDateInfo);
            }

            return reportData;
        }

        /// <summary>
        /// Gets the distinct month date info.
        /// </summary>
        /// <param name="monthDateInfo">The month date info.</param>
        /// <returns>The distinct month date info.</returns>
        private IEnumerable<RedmineMonthDateInfoModel> GetDistinctMonthDateInfo(IEnumerable<RedmineMonthDateInfoModel> monthDateInfo)
        {
            var result = monthDateInfo.GroupBy(m => new { m.Month.MonthName })
                        .Select(g => new RedmineMonthDateInfoModel
                        {
                            Month = new RedmineYearMonthModel
                            {
                                MonthName = g.Key.MonthName
                            },
                            DateStatuses = monthDateInfo.Where(m => m.Month.MonthName == g.Key.MonthName).Select(m => m.DateStatus).ToList()
                        });

            return result;
        }

        /// <summary>
        /// Gets the distinct project resources.
        /// </summary>
        /// <param name="projectResources">The project resources.</param>
        /// <returns>The distinct project resources.</returns>
        private IEnumerable<RedmineProjectResourceModel> GetDistinctProjectResources(IEnumerable<RedmineProjectResourceModel> projectResources)
        {
            var result = projectResources.GroupBy(p => new { p.Employee.NickName, p.Employee.FullName })
                         .Select(g =>
                         {
                             var matchedProjects = projectResources.Where(p => p.Employee.NickName == g.Key.NickName).Select(p => p.Project).ToList();

                             var projectResource = new RedmineProjectResourceModel
                                      {
                                          Employee = new RedmineEmployeeModel
                                          {
                                              NickName = g.Key.NickName,
                                              FullName = g.Key.FullName
                                          },
                                          Projects = matchedProjects,
                                          TotalIssueCount = matchedProjects.Sum(p => p.IssueCount)
                                      };

                             return projectResource;
                         });

            return result;
        }
    }
}
