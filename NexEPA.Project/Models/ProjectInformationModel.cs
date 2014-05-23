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
    using NexEPA.Core.Models;

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

        public string NickName { get; set; }

        public int Permission { get; set; }

        public int TotalOpenEPA { get; set; }

        public int TotalEPA { get; set; }

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
                    if (Milestones.Count == 1)
                    {
                        MilestoneModel item = Milestones[0];
                        milestoneTree = new TreeViewModel() { Id = ProjectId, Label = item.Name, Children = new List<TreeViewModel>() };
                    }
                    else
                    {
                        string labelName = Milestones.Count.ToString() + " milestones";
                        milestoneTree = new TreeViewModel() { Id = ProjectId, Label = labelName, Children = new List<TreeViewModel>() };
                        foreach (MilestoneModel item in Milestones)
                        {
                            if (item != null)
                            {
                                var child = new TreeViewModel();
                                child.Id = item.MilestoneId;
                                child.Label = item.Name;
                                milestoneTree.Children.Add(child);
                            }
                        }
                    }

                    lstTree.Add(milestoneTree);
                }

                return lstTree;
            }
        }
    }
}
