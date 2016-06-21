namespace NexEPA.Employee.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Employee.Models;

    /// <summary>
    /// The interface of Employee Repository.
    /// </summary>
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetAllEmployee(ICriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByProjectId(ICriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByCurrentUserId(ICriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByTeamProject(ICriteria criteria);

        IEnumerable<EmployeeModel> GetRedmineEmployeesByTeamProject(ICriteria criteria);
    }
}
