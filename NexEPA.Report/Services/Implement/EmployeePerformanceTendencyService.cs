// --------------------------------------------------------------------------
// <copyright file="EmployeePerformanceTendencyService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Core.Services;
    using NexEPA.Core.Utilities;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;

    /// <summary>
    /// EmployeePerformanceTendencyService class.
    /// </summary>
    public class EmployeePerformanceTendencyService : BaseService, IEmployeePerformanceTendencyService
    {
        private IEmployeePerformanceTendencyRepository _repository;
        private IReportService _reportService;

        public EmployeePerformanceTendencyService()
            : this(null, null)
        {
        }

        public EmployeePerformanceTendencyService(
            IEmployeePerformanceTendencyRepository repository,
            IReportService reportService)
        {
            _repository = repository ?? new EmployeePerformanceTendencyRepository();
            _reportService = reportService ?? new ReportService();
        }

        /// <summary>
        /// Get employee performance tendency report.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The ReportEmployeePerformanceTendencyModel.</returns>
        public EmployeePerformanceModel GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeePerformanceReport = new EmployeePerformanceModel();

            if (criteria.IsValid())
            {
                EmployeePerformanceModel rawsEmployeePerformanceReport = _repository.GetEmployeePerformanceTendancyReport(criteria);

                if (rawsEmployeePerformanceReport != null)
                {
                    string gravatarString = string.Empty;
                    employeePerformanceReport.EmployeeInformation = rawsEmployeePerformanceReport.EmployeeInformation;

                    var userEmail = employeePerformanceReport.EmployeeInformation.Email;
                    if (userEmail != null)
                    {
                        gravatarString = GravatarImage.HashEmailForGravatar(userEmail);
                    }

                    employeePerformanceReport.EmployeeInformation.Gravatar = gravatarString;

                    var projectHistoryList = _reportService.GetProjectHistory((List<HistoryProjectModel>)rawsEmployeePerformanceReport.RawHistoryProjects);
                    employeePerformanceReport.HistoryProjects = projectHistoryList;
                }
            }

            return employeePerformanceReport;
        }

        /// <summary>
        /// Gets the redmine personal effort.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedminePersonalEffortViewModel class.</returns>
        public RedminePersonalEffortViewModel GetRedminePersonalEffort(GetRedminePersonalEffortCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var report = new RedminePersonalEffortViewModel();

            if (criteria.IsValid())
            {
                report = _repository.GetRedminePersonalEffort(criteria);

                if (report.Projects != null)
                {
                    report.GrandTotalSpentTime = report.Projects.Sum(p => p.TotalSpentTimeOfAllMonths);
                }
            }

            return report;
        }

        /// <summary>
        /// Gets the redmine personal effort detail.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>RedminePersonalEffortDetailViewModel class.</returns>
        public RedminePersonalEffortDetailViewModel GetRedminePersonalEffortDetail(GetRedminePersonalEffortDetailCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var report = new RedminePersonalEffortDetailViewModel();

            if (criteria.IsValid())
            {
                report = _repository.GetRedminePersonalEffortDetail(criteria);
                if (report.ProjectTrackers != null)
                {
                    report.ProjectTrackers = GetDistinctProjectTrackers(report.ProjectTrackers);
                }

                if (report.TrackerMonths != null)
                {
                    report.DistinctTrackerMonths = GetDistinctTrackerMonths(report.TrackerMonths);
                }

                if (report.WorkloadByProjects != null)
                {
                    report.GrandTotalWorkload = report.WorkloadByProjects.Sum(w => w.Workload);
                }

                if (report.WorkloadByMonths != null)
                {
                    report.GrandTotalWorkingDay = report.WorkloadByMonths.Sum(w => w.TotalWorkingDay);
                }

                if (report.PersonalEffortDetails != null)
                {
                    report.ProjectMonthSubTotals = GetSubTotalByProjectMonth(report.PersonalEffortDetails);
                }
            }

            return report;
        }

        /// <summary>
        /// Gets the sub total by project month.
        /// </summary>
        /// <param name="personalEffortDetails">The personal effort details.</param>
        /// <returns>RedmineProjectMonthSubTotalModel collection.</returns>
        private IEnumerable<RedmineProjectMonthSubTotalModel> GetSubTotalByProjectMonth(IEnumerable<RedminePersonalEffortDetailModel> personalEffortDetails)
        {
            var result = personalEffortDetails.GroupBy(p => new
            {
                p.ProjectId,
                p.Year,
                p.Month
            }).Select(p => new RedmineProjectMonthSubTotalModel
            {
                ProjectId = p.Key.ProjectId,
                Year = p.Key.Year,
                Month = p.Key.Month,
                NumberIssue = p.Sum(d => d.NumberIssue),
                SpentTime = p.Sum(d => d.SpentTime)
            });

            return result;
        }

        /// <summary>
        /// Gets the distinct tracker months.
        /// </summary>
        /// <param name="trackerMonths">The tracker months.</param>
        /// <returns>RedmineTrackerMonthModel collection.</returns>
        private IEnumerable<RedmineTrackerMonthModel> GetDistinctTrackerMonths(IEnumerable<RedmineTrackerMonthModel> trackerMonths)
        {
            var result = trackerMonths.GroupBy(t => new
            {
                t.TrackerId,
                t.TrackerName
            }).Select(t => new RedmineTrackerMonthModel
            {
                TrackerId = t.Key.TrackerId,
                TrackerName = t.Key.TrackerName,
                TotalNumberIssue = t.Sum(m => m.YearMonth.NumberIssue),
                TotalSpentTime = t.Sum(m => m.YearMonth.SpentTime)
            });

            return result;
        }

        /// <summary>
        /// Gets the distinct project trackers.
        /// </summary>
        /// <param name="projectTrackers">The project trackers.</param>
        /// <returns>RedmineProjectTrackerModel collection.</returns>
        private IEnumerable<RedmineProjectTrackerModel> GetDistinctProjectTrackers(IEnumerable<RedmineProjectTrackerModel> projectTrackers)
        {
            var result = new List<RedmineProjectTrackerModel>();
            var count = 1;

            foreach (RedmineProjectTrackerModel item in projectTrackers)
            {
                bool isExisting = result.Any(p => p.ProjectId == item.ProjectId);
                if (!isExisting)
                {
                    var matchedProjects = projectTrackers.Where(p => p.ProjectId == item.ProjectId);

                    var project = new RedmineProjectTrackerModel
                    {
                        Index = count++,
                        ProjectId = item.ProjectId,
                        ProjectName = item.ProjectName,
                        TotalNumberIssue = matchedProjects.Sum(p => p.Tracker.NumberIssue),
                        TotalSpentTime = matchedProjects.Sum(p => p.Tracker.SpentTime),
                        Trackers = matchedProjects.Select(p => p.Tracker).ToList()
                    };

                    result.Add(project);
                }
            }

            return result;
        }
    }
}
