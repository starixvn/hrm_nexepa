// -----------------------------------------------------------------------
// <copyright file="TargetEmployeeViewModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.16">Created</change>
// </history>

namespace NexEPA.Target.ViewModels.Target
{
    /// <summary>
    /// Target Employee Item.
    /// </summary>
    public class TargetEmployeeViewModel
    {
        public int EmployeeId { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }
    }
}
