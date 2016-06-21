namespace NexEPA.Report.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Core.Utilities;
    using NexEPA.KPI.Model;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Repositories.Implement;
    using NexEPA.Report.Repositories.Interface;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Report.ViewModels;
    using NexEPA.Target.Models;

    /// <summary>
    /// Report Service.
    /// </summary>
    public class ReportService : BaseService, IReportService
    {
        private IReportRepository _reportRepository;

        private IEncodeService _encodeService;

        public ReportService()
            : this(null, null)
        {
        }

        public ReportService(IReportRepository reportRepository, IEncodeService encodeService)
        {
            _reportRepository = reportRepository ?? new ReportRepository();
            _encodeService = encodeService ?? new EncodeService();
        }

        /// <summary>
        /// Get general information of current employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List DashboardModel.</returns>
        public DashboardModel GetGeneralInformationOfCurrentEmployee(GetGeneralInformationOfCurrentEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var dashboardInformation = new DashboardModel();

            if (criteria.IsValid())
            {
                DashboardModel rawDashboardInformation = _reportRepository.GetGeneralInformationOfCurrentEmployee(criteria);

                if (rawDashboardInformation != null)
                {
                    dashboardInformation.GeneralInformation = rawDashboardInformation.GeneralInformation;

                    dashboardInformation.OpenProjects = rawDashboardInformation.OpenProjects;

                    IEnumerable<HistoryProjectViewModel> projectHistoryList = GetProjectHistory((List<HistoryProjectModel>)rawDashboardInformation.RawHistoryProjects);
                    dashboardInformation.HistoryProjects = projectHistoryList;

                    dashboardInformation.ListKPIs = GetKPIsInformation(rawDashboardInformation.ListKPIs);
                }
            }

            return dashboardInformation;
        }

        /// <summary>
        /// Get project history.
        /// </summary>
        /// <param name="rawHistoryProjects">The history projects.</param>
        /// <returns>List HistoryProjectViewModel.</returns>
        public List<HistoryProjectViewModel> GetProjectHistory(List<HistoryProjectModel> rawHistoryProjects)
        {
            var historyProjects = new List<HistoryProjectViewModel>();

            if (rawHistoryProjects != null && rawHistoryProjects.Any())
            {
                historyProjects = LoopAndGetProjectHistory(rawHistoryProjects, historyProjects);
            }

            return historyProjects;
        }

        /// <summary>
        /// Loop and get project history.
        /// </summary>
        /// <param name="rawHistoryProjects">The list history projects.</param>
        /// <param name="historyProjects">The list history projects view model.</param>
        /// <returns>The list History Project View Model.</returns>
        private List<HistoryProjectViewModel> LoopAndGetProjectHistory(
            List<HistoryProjectModel> rawHistoryProjects,
            List<HistoryProjectViewModel> historyProjects)
        {
            int counter = 1;
            for (int i = 0; i < rawHistoryProjects.Count; i++)
            {
                HistoryProjectModel rawHistoryProjectItem = rawHistoryProjects[i];
                var historyProjectItem = new HistoryProjectViewModel
                {
                    Roles = rawHistoryProjectItem.Roles,
                    Comment = rawHistoryProjectItem.Comment,
                    Evaluator = rawHistoryProjectItem.Evaluator,
                    Project = rawHistoryProjectItem.Project
                };

                List<HistoryProjectModel> sameProjectRows = rawHistoryProjects
                                                                .Where(pro => pro.Project.ProjectId == rawHistoryProjectItem.Project.ProjectId).ToList();

                decimal averageScore = 0;
                decimal totalScore = 0;
                int numberRowsSameProject = 0;

                foreach (HistoryProjectModel project in sameProjectRows)
                {
                    string plainScoreText = !string.IsNullOrEmpty(project.Score) ? _encodeService.Decode(project.Score) : "0";
                    plainScoreText = CommonHelper.PercentToStarNumber(plainScoreText);
                    decimal projectScore = CommonHelper.ConvertDecimal(plainScoreText);

                    if (projectScore > 0)
                    {
                        totalScore += projectScore;
                        numberRowsSameProject += 1;
                    }

                    var target = CreateTarget(project, plainScoreText, ref counter);

                    historyProjectItem.TargetList.Add(target);
                    if (project != rawHistoryProjectItem)
                    {
                        rawHistoryProjects.Remove(project);
                    }
                }

                averageScore = numberRowsSameProject > 0 ? totalScore / numberRowsSameProject : 0;

                historyProjectItem.Score = averageScore;
                historyProjects.Add(historyProjectItem);
            }

            return historyProjects;
        }

        private TargetModel CreateTarget(HistoryProjectModel project, string score, ref int counter)
        {
            var target = new TargetModel
            {
                TargetId = project.TargetId,
                Name = project.Name,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Score = score,
                Index = counter++
            };

            return target;
        }

        /// <summary>
        /// Get KPIs information.
        /// </summary>
        /// <param name="listKPIs">The list KPIs.</param>
        /// <returns>The enumerator KPI model.</returns>
        private IEnumerable<KPIModel> GetKPIsInformation(IEnumerable<KPIModel> listKPIs)
        {
            IEnumerable<KPIModel> groupKPIs = from kpi in listKPIs
                                              group kpi by kpi.KPIId into groupKPI
                                              select new KPIModel
                                              {
                                                  KPIId = groupKPI.Key,
                                                  Name = groupKPI.First().Name,
                                                  EvaluateTime = groupKPI.Count(),
                                                  AverageScore = CalculateAverageScoreOfKPI(groupKPI)
                                              };

            return groupKPIs;
        }

        /// <summary>
        /// Calculate average score of KPI.
        /// </summary>
        /// <param name="groupKPI">The group KPI.</param>
        /// <returns>The average score.</returns>
        private decimal CalculateAverageScoreOfKPI(IGrouping<int, KPIModel> groupKPI)
        {
            decimal totalScore = 0;
            int count = 0;

            foreach (KPIModel kpi in groupKPI)
            {
                string itemScoreText = !string.IsNullOrEmpty(kpi.Score) ? _encodeService.Decode(kpi.Score).Replace("_" + kpi.KPIId, string.Empty) : string.Empty;
                decimal score = CommonHelper.ConvertDecimal(itemScoreText);

                if (score > 0)
                {
                    totalScore += score;
                    count++;
                }
            }

            decimal averageScore = count > 0 ? totalScore / count : 0;
            return averageScore;
        }
    }
}
