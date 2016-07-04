// --------------------------------------------------------------------------
// <copyright file="ResourcePlanningReportService.cs" company="">
// Copyright (c) . All rights reserved.
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
                reportData.MonthInfo = GetMonthInfo(reportData.DateInfo);
            }

            return reportData;
        }

        /// <summary>
        /// Gets the month info.
        /// </summary>
        /// <param name="dateInfo">The date info.</param>
        /// <returns>The month info.</returns>
        private IEnumerable<RedmineYearMonthModel> GetMonthInfo(IEnumerable<RedmineMonthDateInfoModel> dateInfo)
        {
            var result = dateInfo.GroupBy(m => new { m.Month.MonthName })
                        .Select(g => new RedmineYearMonthModel
                        {
                            MonthName = g.Key.MonthName,
                            DayCount = g.GroupBy(m => new { m.DateStatus.DayName }).Count()
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
