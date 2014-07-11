// --------------------------------------------------------------------------
// <copyright file="MemberEPARepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
//   <change who="Josh.Phan" date="2014.4.22">Added - GetEPAByEmployee</change>
// </history>

namespace NexEPA.MemberEPA.Repositories.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Utilities;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MyEPA;
    using NexEPA.MemberEPA.Repositories.Interface;
    using NexEPA.Target.Models;

    /// <summary>
    /// The implement of Member EPA Repository.
    /// </summary>
    public class MemberEPARepository : IMemberEPARepository
    {
        /// <summary>
        /// Get EPA Info.
        /// </summary>
        /// <param name="criteria">Criteria to query DB.</param>
        /// <returns>EPAInfo Model.</returns>
        public EPAInfoModel GetEPAInfo(ICriteria criteria)
        {
            EPAInfoModel data = new EPAInfoModel();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    data = multi.Read<EPAInfoModel>().FirstOrDefault();
                    if (data != null)
                    {
                        data.ProjectRoles = multi.Read<EPAProjectRoleModel>().ToList();
                        List<KPIModel> kpis = multi.Read<KPIModel, KPIGroupModel, KPIModel>(
                            (inf, gr) =>
                            {
                                inf.Group = gr;
                                return inf;
                            },
                            "KPIGroupId").ToList();

                        List<GroupingKPIModel> groupData = (from kpi in kpis
                                                            group kpi by kpi.Group.Name into kpi
                                                            select new GroupingKPIModel()
                                                            {
                                                                KPIGroupName = kpi.Key,
                                                                KPIGroupItems = kpi.ToList()
                                                            }).ToList();

                        data.KPIInfo = groupData;

                        string userEmail = data.Email;
                        if (userEmail != null)
                        {
                            data.Gravatar = GravatarImage.HashEmailForGravatar(data.Email);
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// Get EPA by employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The IENumerable MemberEPAModel.</returns>
        public IEnumerable<MemberEPAModel> GetEPAByEmployee(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<MemberEPAModel, Target, Project, Milestone, Evaluator, MemberEPAModel>(
                    (me, ta, pro, mi, ev) =>
                    {
                        me.Target = ta;
                        me.Project = pro;
                        me.Milestone = mi;
                        me.Evaluator = ev;
                        return me;
                    },
                    "TargetId,ProjectId,MilestoneId,EvaluatorId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get EPA of employee by manager.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The IEnumerable MemberEPAModel.</returns>
        public IEnumerable<MemberEPAModel> GetEPAOfEmployeeByManager(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<MemberEPAModel, Target, Project, Milestone, Evaluator, MemberEPAModel>(
                    (me, ta, pro, mi, ev) =>
                    {
                        me.Target = ta;
                        me.Project = pro;
                        me.Milestone = mi;
                        me.Evaluator = ev;
                        return me;
                    },
                    "TargetId,ProjectId,MilestoneId,EvaluatorId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get Appraisal Form.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Appraisal Form model.</returns>
        public IEnumerable<AppraisalFormModel> GetAppraisalForm(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<AppraisalFormModel, EmployeeModel, EPATypeModel, AppraisalFormModel>(
                    (app, emp, epa) =>
                    {
                        app.Employee = emp;
                        app.EPAType = epa;
                        return app;
                    },
                    "EmployeeId,EPATypeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get Appraisal Form by Id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Appraisal Form model.</returns>
        public AppraisalFormModel GetAppraisalFormById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (GridReader multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    AppraisalFormModel appraisal = multi.Read<AppraisalFormModel, EmployeeModel, EPATypeModel, AppraisalFormModel>(
                    (app, emp, epa) =>
                    {
                        app.Employee = emp;
                        app.EPAType = epa;
                        return app;
                    },
                    "EmployeeId,EPATypeId").FirstOrDefault();

                    appraisal.EPAProcess = multi.Read<EPAProcessModel, EmployeeModel, EPAProcessModel>(
                        (epa, eva) =>
                        {
                            epa.Evaluator = eva;
                            return epa;
                        },
                        "EvaluatorId");

                    return appraisal;
                }
            }
        }
    }
}
