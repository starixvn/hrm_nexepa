namespace NexEPA.KPI.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.ViewModels;

    /// <summary>
    /// The interface of KPI service.
    /// </summary>
    public interface IKPIService
    {
        List<KPIViewModel> GetKPI(GetKPICriteria criteria);

        KPIViewModel GetKPIById(GetKPIByIdCriteria criteria);

        CommandResult InsertKPI(InsertKPICriteria criteria);

        CommandResult UpdateKPI(UpdateKPICriteria criteria);

        CommandResult DeleteKPI(DeleteKPICriteria criteria);

        IEnumerable<KPIViewModel> GetAllKPIByKPIGroup(GetAllKPIByKPIGroupCriteria criteria);

        CommandResult UpdateKPIStatus(UpdateKPIStatusCriteria criteria);
    }
}
