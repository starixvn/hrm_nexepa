// --------------------------------------------------------------------------
// <copyright file="ResourcePlanningReportRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;
    using NexEPA.Report.Models.ResourcePlanningReport;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// ResourcePlanningReportRepository class.
    /// </summary>
    public class ResourcePlanningReportRepository : IResourcePlanningReportRepository
    {
        /// <summary>
        /// Gets the redmine resource planning.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The redmine resource planning.</returns>
        public RedmineResourcePlanningViewModel GetRedmineResourcePlanning(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var report = new RedmineResourcePlanningViewModel();

                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var resourcePlannings = GetDetailResourcePlanning(multi);

                    var projectResources = GetProjectResources(multi);

                    var resourceDateStatuses = GetResourceDateStatuses(multi);

                    var monthDateInfo = GetMonthDateInfo(multi);

                    report.DetailResourcePlannings = resourcePlannings;
                    report.ProjectResources = projectResources;
                    report.ResourceDateStatuses = resourceDateStatuses;
                    report.DateInfo = monthDateInfo;
                }

                return report;
            }
        }

        /// <summary>
        /// Gets the month date information.
        /// </summary>
        /// <param name="multi">The multi reader.</param>
        /// <returns>The month date information.</returns>
        private static IEnumerable<RedmineMonthDateInfoModel> GetMonthDateInfo(GridReader multi)
        {
            var monthDateInfo = multi.Read<RedmineYearMonthModel, RedmineDateStatusModel, RedmineMonthDateInfoModel>(
                                    (month, dateStatus) =>
                                    {
                                        var monthDate = new RedmineMonthDateInfoModel
                                        {
                                            Month = month,
                                            DateStatus = dateStatus
                                        };

                                        return monthDate;
                                    },
                                    "DayName");

            return monthDateInfo;
        }

        /// <summary>
        /// Gets the resource date statuses.
        /// </summary>
        /// <param name="multi">The multi reader.</param>
        /// <returns>The resource date statuses.</returns>
        private static IEnumerable<RedmineResourceDateStatusModel> GetResourceDateStatuses(GridReader multi)
        {
            var resourceDateStatuses = multi.Read<RedmineEmployeeModel, RedmineDateStatusModel, RedmineResourceDateStatusModel>(
                                        (employee, dateStatus) =>
                                        {
                                            var resourceDateStatus = new RedmineResourceDateStatusModel
                                            {
                                                Employee = employee,
                                                DateStatus = dateStatus
                                            };

                                            return resourceDateStatus;
                                        },
                                        "DayName");

            return resourceDateStatuses;
        }

        /// <summary>
        /// Gets the project resources.
        /// </summary>
        /// <param name="multi">The multi reader.</param>
        /// <returns>The project resources.</returns>
        private static IEnumerable<RedmineProjectResourceModel> GetProjectResources(GridReader multi)
        {
            var projectResources = multi.Read<RedmineEmployeeModel, RedmineProjectModel, RedmineProjectResourceModel>(
                                    (employee, project) =>
                                    {
                                        var projectResource = new RedmineProjectResourceModel
                                        {
                                            Employee = employee,
                                            Project = project
                                        };

                                        return projectResource;
                                    },
                                    "ProjectId");

            return projectResources;
        }

        /// <summary>
        /// Gets the detail resource planning.
        /// </summary>
        /// <param name="multi">The multi reader.</param>
        /// <returns>The detail resource planning.</returns>
        private static IEnumerable<RedmineDetailResourcePlanningModel> GetDetailResourcePlanning(GridReader multi)
        {
            var resourcePlannings = multi.Read<RedmineEmployeeModel, RedmineProjectModel, RedmineIssueModel, RedmineDetailResourcePlanningModel>(
                                    (employee, project, issue) =>
                                    {
                                        var resourcePlanning = new RedmineDetailResourcePlanningModel
                                        {
                                            Employee = employee,
                                            Project = project,
                                            Issue = issue
                                        };

                                        return resourcePlanning;
                                    },
                                    "ProjectId,IssueId");

            return resourcePlannings;
        }
    }
}
