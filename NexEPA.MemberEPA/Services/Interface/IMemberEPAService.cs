// --------------------------------------------------------------------------
// <copyright file="IMemberEPAService.cs" company="">
namespace NexEPA.MemberEPA.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MemberEPA;
    using NexEPA.MemberEPA.Models.MyEPA;

    /// <summary>
    /// Interface Member EPA Service.
    /// </summary>
    public interface IMemberEPAService
    {
        EPAInfoModel GetEPAInfo(GetEPAInfoCriteria criteria);

        List<MemberEPAModel> GetEPAByEmployee(GetEPAByEmployeeCriteria criteria);

        CommandResult AddOrUpdateEmployeeEPA(AddOrUpdateEPAScoreCriteria criteria);

        List<GroupEPAOfMemberModel> GetEPAOfEmployeeByManager(GetEPAOfAllEmployeeByManagerCriteria criteria);

        CommandResult UpdateEPAStatus(UpdateEPAStatusCriteria criteria);

        int CheckEmployeePermission(GetEmployeePermissionCriteria criteria);

        IEnumerable<AppraisalFormModel> GetAppraisalForm(GetAppraisalFormCriteria criteria);

        CommandResult InsertAppraisalForm(InsertAppraisalFormCriteria criteria);

        IEnumerable<ProcessPeriodModel> GetAllProcessPeriod(GetAllProcessPeriodCriteria criteria);

        AppraisalFormModel GetAppraisalFormById(GetAppraisalFormByIdCriteria criteria);

        CommandResult UpdateAppraisalForm(UpdateAppraisalFormCriteria criteria);

        CommandResult DeleteAppraisalForm(DeleteAppraisalFormCriteria criteria);

        CommandResult UpdateStatusAppraisalForm(UpdateStatusAppraisalFormCriteria criteria);

        CommandResult UpdateKPIScore(UpdateKPIScoreCriteria criteria);

        int GetUserPermissionOnTarget(GetUserPermissionOnTargetCriteria criteria);

        string Encode(string plainText);

        string Decode(string cipherText);

        string ConvertKPIInfoToXML(List<GroupingKPIModel> kpiInfo);

        CommandResult UpdateCommentEPA(UpdateEPACommentsCriteria criteria);

        string ConvertEPACommentsToXML(IEnumerable<EmployeeModel> employees);
    }
}