namespace NexEPA.Authority.Services.Interface
{
    using System.Collections.Generic;
    using Authority.Criteria;
    using Authority.Models;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Criteria;

    /// <summary>
    /// The interface of Authority service.
    /// </summary>
    public interface IAuthorityService
    {
        IEnumerable<FunctionModel> GetFunctionByRole(GetFunctionByRoleCriteria criteria);

        IEnumerable<RoleSystemModel> GetAllSystemRole(GetAllSystemRoleCriteria criteria);

        CommandResult AssignPermissionToRole(AssignPermissionToRoleCriteria criteria);

        IEnumerable<RoleSystemModel> GetRolesByEmployeeId(GetRoleByEmployeeIdCriteria criteria);

        CommandResult UpdateRole(UpdateRoleOfEmployeeCriteria criteria);

        bool IsOnlyMember(EmployeeIsOnlyMemberCriteria criteria);

        bool CheckRightToViewPersonReportOfEmployee(int employeeId, GetAllEmployeeByCurrentUserIdCriteria criteria);
    }
}
