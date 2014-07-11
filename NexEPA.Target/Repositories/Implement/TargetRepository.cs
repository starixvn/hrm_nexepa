// -----------------------------------------------------------------------
// <copyright file="TargetRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Repositories.Implement
{
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Project.Models;
    using NexEPA.Target.Models;
    using NexEPA.Target.Repositories.Interface;

    /// <summary>
    /// The implement of Target Repository.
    /// </summary>
    public class TargetRepository : ITargetRepository
    {
        /// <summary>
        /// Get target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Target Model.</returns>
        public IEnumerable<TargetModel> GetTarget(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<TargetModel, TargetType, ProjectModel, MilestoneModel, EmployeeModel, TargetModel>(
                    (tm, tt, pro, mi, emp) => 
                    {
                        tm.TargetType = tt;
                        tm.Project = pro;
                        tm.Milestone = mi;
                        tm.ProjectManager = emp;
                        return tm;
                    },
                    "TargetTypeId,ProjectId,MilestoneId,EmployeeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get Target by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Target Model.</returns>
        public TargetModel GetTargetById(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.QueryEntity<TargetModel, TargetType, ProjectModel, MilestoneModel, EmployeeModel, TargetModel>(
                    (tm, tt, pro, mi, emp) =>
                    {
                        tm.TargetType = tt;
                        tm.Project = pro;
                        tm.Milestone = mi;
                        tm.ProjectManager = emp;
                        return tm;
                    },
                    "TargetTypeId,ProjectId,MilestoneId,EmployeeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get KPI by Target ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator KPI Model.</returns>
        public IEnumerable<KPIModel> GetKPIByTargetId(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<KPIModel, KPIGroupModel, TargetType, KPIModel>(
                    (kpi, gr, tt) =>
                    {
                        kpi.Group = gr;
                        kpi.TargetType = tt;
                        return kpi;
                    },
                    "KPIGroupId,TargetTypeId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get Employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Employee Model.</returns>
        public IEnumerable<EmployeeModel> GetEmployee(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<EmployeeModel, JobTitle, Group, EmployeeModel>(
                    (emp, job, gr) =>
                    {
                        emp.JobTitle = job;
                        emp.Group = gr;
                        return emp;
                    },
                    "JobTitleId,GroupId",
                    criteria.GetSpParams());
            }
        }

        /// <summary>
        /// Get employee information by KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The temporary Employees and EPA Model.</returns>
        public TemporaryEPAAndEmployeeModel GetEmployeeByKPI(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(criteria.GetSpParams()))
                {
                    var temporaryEPA = new TemporaryEPAAndEmployeeModel();

                    temporaryEPA.Employees = multi.Read<EmployeeModel, JobTitle, Group, EmployeeModel>(
                        (emp, job, gr) =>
                        {
                            emp.JobTitle = job;
                            emp.Group = gr;
                            return emp;
                        },
                        "JobTitleId,GroupId");

                    temporaryEPA.RawEPAs = multi.Read<TemporaryEPAModel>();

                    return temporaryEPA;
                }
            }
        }
    }
}
