// -----------------------------------------------------------------------
// <copyright file="EmployeeServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.03">Created</change>
// </history>

namespace NexEPA.Employee.Services.Implement
{
    using System.Collections.Generic;
    using NexEPA.Core.Services;
    using NexEPA.Employee.Constant;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services.Interface;
    using NexEPA.Employee.ViewModels;

    /// <summary>
    /// The implement of Employee services.
    /// </summary>
    public class EmployeeServices : BaseService, IEmployeeServices
    {
        /// <summary>
        /// Get all employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list Employee Item.</returns>
        public List<EmployeeItem> GetAllEmployee(GetAllEmployeeCriteria criteria)
        {
            IEnumerable<EmployeeInfo> rawEmployees = GetData<EmployeeInfo>(criteria);
            var employees = new List<EmployeeItem>();

            foreach (var employee in rawEmployees)
            {
                var employeeItem = new EmployeeItem
                {
                    EmployeeId = employee.EmployeeId,
                    Code = employee.Code,
                    FullName = employee.FullName,
                    NickName = employee.NickName,
                    Email = employee.Email,
                    Skype = employee.Skype,
                    JoinedDate = employee.JoinedDate.ToString(EmployeeConstant.ShortDateStringFormat),
                    GroupId = employee.GroupId,
                    GroupName = "Virtual Group Name",
                    //// TODO : GroupName = GetGroupNameById(GroupId)
                    JobTitleId = employee.JobTitleId,
                    JobTitleName = "Virtual Job Title"
                    //// TODO : JobTitleName = GetJobTitleById(JobTitleId)
                };

                employees.Add(employeeItem);
            }

            return employees;
        }
    }
}
