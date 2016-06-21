namespace NexEPA.KPI.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The interface of KPI Group repository.
    /// </summary>
    public interface IKPIGroupRepository
    {
        IEnumerable<KPIGroupModel> GetAllKPIGroup(ICriteria criteria);

        KPIGroupModel GetKPIGroupById(ICriteria criteria);
    }
}
