// --------------------------------------------------------------------------
// <copyright file="TargetTemplateModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Target.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// Target Template Model.
    /// </summary>
    public class TargetTemplateModel
    {
        private const string _KPIItem = "<KPI KPIId='{0}' KPIGroupId='{1}' Weight='{2}'></KPI>";
        private const string _BeginRootTag = "<ROOT>";
        private const string _EndRootTag = "</ROOT>";

        public int TargetTemplateId { get; set; }

        [Required(ErrorMessage = "The field Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "The field Target Type is required.")]
        public byte? TargetTypeId { get; set; }

        public string TargetTypeName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public bool IsDisabled { get; set; }

        public int KPIs { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int ModifiedBy { get; set; }

        public List<TargetTemplateKPIModel> KPIIds { get; set; }

        public string CreatedNickName { get; set; }

        public string CreatedFullName { get; set; }

        public string ModifiedNickName { get; set; }

        public string ModifiedFullName { get; set; }

        public string ConvertKPIItemToXML
        {
            get
            {
                var value = new StringBuilder(_BeginRootTag);
                foreach (var item in KPIIds)
                {
                    value.AppendFormat(_KPIItem, item.KPIId, item.KPIGroupId, item.Weight);
                }

                return value.Append(_EndRootTag).ToString();
            }
        }
    }
}
