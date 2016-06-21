namespace NexEPA.KPI.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The interface of KPI repository.
    /// </summary>
    public interface IKPIRepository
    {
        IEnumerable<KPIModel> GetKPI(ICriteria criteria);

        IEnumerable<KPIModel> GetKPIById(ICriteria criteria);
    }
}
