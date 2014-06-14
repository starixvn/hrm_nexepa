// -----------------------------------------------------------------------
// <copyright file="TargetEmployeeSettingModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.14">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    /// <summary>
    /// Target Employee Setting model.
    /// </summary>
    public class TargetEmployeeSettingModel
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public int? TargetTemplateId { get; set; }

        public string Name { get; set; }
    }
}
