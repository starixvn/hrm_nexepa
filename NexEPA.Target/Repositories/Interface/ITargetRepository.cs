namespace NexEPA.Target.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Target.Models;

    /// <summary>
    /// The interface of Target Repository.
    /// </summary>
    public interface ITargetRepository
    {
        IEnumerable<TargetModel> GetTarget(ICriteria criteria);

        TargetModel GetTargetById(ICriteria criteria);

        IEnumerable<KPIModel> GetKPIByTargetId(ICriteria criteria);

        IEnumerable<EmployeeModel> GetEmployee(ICriteria criteria);

        TemporaryEPAAndEmployeeModel GetEmployeeByKPI(ICriteria criteria);

        IList<ProjectTargetModel> GetRedmineProjectTarget(ICriteria criteria);

        CommandResult SyncTargetFromRedmineFeature(ICriteria criteria);
    }
}
