// --------------------------------------------------------------------------
// <copyright file="EPAInfoModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// EPA Info Model.
    /// </summary>
    public class EPAInfoModel
    {
        private const string _EmployeeEPAScore = "<KPI KPIId='{0}' KPIGroupId='{1}' ScoreId='{2}' Comment='{3}'></KPI>";
        private const string _BeginRootTag = "<ROOT>";
        private const string _EndRootTag = "</ROOT>";

        public int TargetId { get; set; }

        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int MilestoneId { get; set; }

        public string MilestoneName { get; set; }

        public int ProjectManagerId { get; set; }

        public string PMNickName { get; set; }

        public string PMFullName { get; set; }

        public string Comment { get; set; }

        public int Status { get; set; }

        public string Gravatar { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? Modifier { get; set; }

        public string ModifierNickName { get; set; }

        public string ModifierFullName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal FinalScore { get; set; }

        public int Permission { get; set; }

        public List<EPAProjectRoleModel> ProjectRoles { get; set; }

        public List<GroupingKPIModel> KPIInfo { get; set; }

        public string ConvertKPIInfoToXML
        {
            get
            {
                if (KPIInfo != null)
                {
                    var value = new StringBuilder(_BeginRootTag);
                    foreach (var kpi in KPIInfo)
                    {
                        foreach (var item in kpi.KPIGroupItems)
                        {
                            value.AppendFormat(_EmployeeEPAScore, item.KPIId, item.Group.KPIGroupId, item.Score, item.Comment);
                        }
                    }

                    return value.Append(_EndRootTag).ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
