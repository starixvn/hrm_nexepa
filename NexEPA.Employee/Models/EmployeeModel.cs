// --------------------------------------------------------------------------
// <copyright file="EmployeeModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.5.7">Created</change>
// </history>

namespace NexEPA.Employee.ViewModels
{
    using NexEPA.Core.Constants;
    using NexEPA.Core.Models;
    using NexEPA.Core.ViewModels;

    /// <summary>
    /// Employee ViewModel.
    /// </summary>
    public class EmployeeModel : IViewModel
    {
        public EmployeeModel(IModel model)
        {
            this.SetInfo(model);
        }

        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string JoinedDate { get; set; }

        public Group Group { get; set; }

        public JobTitle JobTitle { get; set; }

        /// <summary>
        /// Function to set data info for object.
        /// </summary>
        /// <param name="model">The Model.</param>
        public void SetInfo(IModel model)
        {
            var employee = (Employee)model;
            EmployeeId = employee.EmployeeId;
            Code = employee.Code;
            FullName = employee.FullName;
            NickName = employee.NickName;
            Email = employee.Email;
            Skype = employee.Skype;
            JoinedDate = employee.JoinedDate.HasValue
                ? employee.JoinedDate.Value.ToString(Constants.ShortDateStringFormat)
                : string.Empty;
            Group = employee.Group;
            JobTitle = employee.JobTitle;
        }
    }
}
