// -----------------------------------------------------------------------
// <copyright file="UpdateGroupProjectCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.17">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.Data.Repository;
    using NexEPA.Project.Models.GroupProject;

    /// <summary>
    /// Update group project criteria.
    /// </summary>
    public class UpdateGroupProjectCriteria : CriteriaBase
    {
        private const string _ProjectTemplate = "<Project ProjectId='{0}' DatabaseId='{1}'></Project>";

        public int GroupProjectId { get; set; }

        public string Name { get; set; }

        public int ModifiedBy { get; set; }

        [SpParam(Ignored = true)]
        public IList<GroupProjectDetailModel> Projects { get; set; }

        public string ListProject
        {
            get
            {
                var value = new StringBuilder("<ROOT>");

                if (Projects != null)
                {
                    foreach (GroupProjectDetailModel project in Projects)
                    {
                        value.AppendFormat(_ProjectTemplate, project.ProjectId, project.DatabaseId);
                    }
                }

                return value.Append("</ROOT>").ToString();
            }
        }

        public override string GetSettingKey()
        {
            return "GroupProject_Update";
        }

        public override bool IsValid()
        {
            return GroupProjectId > 0;
        }
    }
}