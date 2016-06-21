namespace NexEPA.Authority.Services.Implement
{
    using System.Collections.Generic;
    using System.Linq;
    using Authority.Criteria;
    using Authority.Models;
    using Authority.Services.Interface;
    using Fanex.Common.Helper;
    using NexEPA.Authority.Repositories;
    using NexEPA.Authority.Repositories.Implement;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Repositories.Implement;
    using NexEPA.Employee.Repositories.Interface;

    /// <summary>
    /// The implement of Authority service.
    /// </summary>
    public class AuthorityService : BaseService, IAuthorityService
    {
        private IAuthorityRepository _repository;
        private IEmployeeRepository _employeeRepository;

        public AuthorityService()
            : this(null, null)
        {
        }

        public AuthorityService(IAuthorityRepository repository, IEmployeeRepository employeeRepository)
        {
            _repository = repository ?? new AuthorityRepository();
            _employeeRepository = employeeRepository ?? new EmployeeRepository();
        }

        /// <summary>
        /// Get all function.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Function Model.</returns>
        public IEnumerable<FunctionModel> GetFunctionByRole(GetFunctionByRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<FunctionModel> functionGroup = null;

            if (criteria.IsValid())
            {
                IEnumerable<FunctionModel> rawFunctions = GetData<FunctionModel>(criteria);
                functionGroup = rawFunctions.Where<FunctionModel>(func => func.ParentFunctionId == null);

                foreach (FunctionModel rawParentFunctionItem in functionGroup)
                {
                    rawParentFunctionItem.ChildFunctions = GetFunctionByRoleRecursion(rawFunctions, rawParentFunctionItem);
                }
            }

            return functionGroup;
        }

        /// <summary>
        /// Get all role system.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Role System Model.</returns>
        public IEnumerable<RoleSystemModel> GetAllSystemRole(GetAllSystemRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<RoleSystemModel> roles = null;

            if (criteria.IsValid())
            {
                roles = GetData<RoleSystemModel>(criteria);
            }

            return roles;
        }

        /// <summary>
        /// Assign permission to role.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult AssignPermissionToRole(AssignPermissionToRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Insert(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Get roles by employee ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Role System Model.</returns>
        public IEnumerable<RoleSystemModel> GetRolesByEmployeeId(GetRoleByEmployeeIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<RoleSystemModel> roles = null;

            if (criteria.IsValid())
            {
                roles = GetData<RoleSystemModel>(criteria);
            }

            return roles;
        }

        /// <summary>
        /// Update role.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateRole(UpdateRoleOfEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }        

        /// <summary>
        /// Determines whether [is only member] [the specified criteria].
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return true if employee has only member role otherwise return false.</returns>
        public bool IsOnlyMember(EmployeeIsOnlyMemberCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            bool isOnlyMember = true;

            if (criteria.IsValid())
            {
                isOnlyMember = _repository.IsOnlyMember(criteria);
            }

            return isOnlyMember;
        }

        /// <summary>
        /// Checks the right to view person report of employee.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Return true if has the right otherwise return false.</returns>
        public bool CheckRightToViewPersonReportOfEmployee(int employeeId, GetAllEmployeeByCurrentUserIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            bool hasRight = false;

            if (criteria.IsValid())
            {
                IEnumerable<EmployeeModel> employees = _employeeRepository.GetEmployeeByCurrentUserId(criteria);

                hasRight = employees.Any(e => e.EmployeeId == employeeId);
            }

            return hasRight;
        }

        /// <summary>
        /// Get function by role recursion.
        /// </summary>
        /// <param name="rawFunctions">The enumerator contain all functions.</param>
        /// <param name="rawParentFunctionItem">The current parent function.</param>
        /// <returns>The list Function Model.</returns>
        private List<FunctionModel> GetFunctionByRoleRecursion(
            IEnumerable<FunctionModel> rawFunctions,
            FunctionModel rawParentFunctionItem)
        {
            List<FunctionModel> rawChildFunctions = rawFunctions.Where(func => func.ParentFunctionId == rawParentFunctionItem.FunctionId).ToList();

            if (rawChildFunctions != null && rawChildFunctions.Count() > 0)
            {
                foreach (var rawChildFunctionItem in rawChildFunctions)
                {
                    rawChildFunctionItem.ChildFunctions = GetFunctionByRoleRecursion(rawFunctions, rawChildFunctionItem);
                }
            }

            return rawChildFunctions;
        }
    }
}
