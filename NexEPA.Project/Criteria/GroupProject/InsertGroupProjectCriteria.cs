// -----------------------------------------------------------------------
// <copyright file="InsertGroupProjectCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.16">Created</change>
// </history>

namespace NexEPA.Project.Criteria.GroupProject
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.Data.Repository;
    using NexEPA.Core.Extensions;
    using NexEPA.Project.Models.GroupProject;

    /// <summary>
    /// Insert group project criteria.
    /// </summary>
    public class InsertGroupProjectCriteria : CriteriaBase
    {
        private const string _ProjectTemplate = "<Project ProjectId='{0}' DatabaseId='{1}'></Project>";

        public string Name { get; set; }

        public int CreatedBy { get; set; }

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
            return "GroupProject_Insert";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}