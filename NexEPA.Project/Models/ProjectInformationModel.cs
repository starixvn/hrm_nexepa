// --------------------------------------------------------------------------
// <copyright file="ProjectInformationModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.5.6">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NexEPA.Core.Model.Project;

    /// <summary>
    /// Project Information Model.
    /// </summary>
    public class ProjectInformationModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public int ProjectManager { get; set; }

        public int Members { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }

        public string FullName { get; set; }

        public bool IsUpdateStatus { get; set; }

        public List<MilestoneModel> Milestones { get; set; }

        public List<TreeViewModel> MilestoneTreeView
        {
            get
            {
                List<TreeViewModel> lstTree = null;
                TreeViewModel milestoneTree = null;
                if (Milestones.FirstOrDefault() != null)
                {
                    lstTree = new List<TreeViewModel>();
                    milestoneTree = new TreeViewModel() { Id = ProjectId, Label = Name, Children = new List<TreeViewModel>() };
                    foreach (var item in Milestones)
                    {
                        if (item != null)
                        {
                            TreeViewModel child = new TreeViewModel();
                            child.Id = item.MilestoneId;
                            child.Label = item.Name;
                            milestoneTree.Children.Add(child);
                        }
                    }

                    lstTree.Add(milestoneTree);
                }

                return lstTree;
            }
        }
    }
}
