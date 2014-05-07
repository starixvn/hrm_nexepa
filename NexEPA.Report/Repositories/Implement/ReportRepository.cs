// -----------------------------------------------------------------------
// <copyright file="ReportRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Repositories.Interface;

    /// <summary>
    /// The implement of Report Repository.
    /// </summary>
    public class ReportRepository : IReportRepository
    {
        /// <summary>
        /// Get general information of current employee that used in dashboard.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List Object.</returns>
        public List<object> GetGeneralInformationOfCurrentEmployee(ICriteria criteria)
        {
            var result = new List<object>();
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var generalInformationList = (List<GeneralInformationOfProjectModel>)multi.Read<GeneralInformationOfProjectModel>();
                    var generalInformation = generalInformationList.Count > 0 ? generalInformationList[0] : new GeneralInformationOfProjectModel();
                    result.Add(generalInformation);

                    IEnumerable<OpenProjectModel> openProjects = multi.Read<OpenProjectModel, ProjectModel, EvaluatorModel, OpenProjectModel>(
                        (opPro, pro, eva) =>
                        {
                            opPro.Project = pro;
                            opPro.Evaluator = eva;
                            return opPro;
                        },
                        "ProjectId,EmployeeId");
                    result.Add(openProjects);

                    IEnumerable<HistoryProjectModel> historyProjects = 
                        multi.Read<HistoryProjectModel, ProjectModel, EvaluatorModel, MilestoneModel, HistoryProjectModel>(
                        (proHis, pro, eva, mil) =>
                        {
                            proHis.Project = pro;
                            proHis.Evaluator = eva;
                            proHis.Milestone = mil;
                            return proHis;
                        },
                        "ProjectId,EmployeeId,MilestoneId");
                    result.Add(historyProjects);

                    var listKPIs = (List<KPIInformationModel>)multi.Read<KPIInformationModel>();
                    var listKPI = listKPIs.Count > 0 ? listKPIs[0] : new KPIInformationModel();
                    result.Add(listKPI);
                }
            }

            return result;
        }
    }
}
