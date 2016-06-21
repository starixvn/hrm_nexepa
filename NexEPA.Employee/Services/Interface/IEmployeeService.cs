namespace NexEPA.Employee.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;

    /// <summary>
    /// The interface of Employee service.
    /// </summary>
    public interface IEmployeeService
    {
        IEnumerable<EmployeeModel> GetAllEmployee(GetAllEmployeeCriteria criteria);

        IEnumerable<Group> GetAllEmployeeGroup(GetAllEmployeeGroupCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByProjectId(GetEmployeeByProjectIdCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByRole(GetEmployeeByRoleCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(GetAllEmployeeByCurrentUserIdCriteria criteria);

        EmployeeModel GetEmployeeById(GetEmployeeByIdCriteria criteria);

        IEnumerable<EmployeeModel> GetByGroup(GetEmployeeByGroupCriteria criteria);

        IEnumerable<EmployeeModel> GetByDepartment(GetEmployeeByDepartmentCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByTeamProject(GetEmployeeByTeamProjectCriteria criteria);

        IEnumerable<Group> GetAllEmployeeGroupByActorUser(GetAllEmployeeGroupByActorUserCriteria criteria);

        IEnumerable<EmployeeModel> GetRedmineEmployeesByTeamProject(GetRedmineEmployeesByTeamProjectCriteria criteria);

        IEnumerable<EmployeeModel> GetRedmineEmployeesByTeamGroupProject(GetRedmineEmployeesByTeamGroupProjectCriteria criteria);
    }
}
