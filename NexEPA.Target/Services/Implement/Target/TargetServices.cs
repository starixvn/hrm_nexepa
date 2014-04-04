// -----------------------------------------------------------------------
// <copyright file="TargetServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Implement.Target
{
    using System.Collections.Generic;
    using NexEPA.Core.Services;
    using NexEPA.Target.Services.Interface.Target;
    using NexEPA.Target.ViewModels.Target;

    /// <summary>
    /// The implement of Target Services.
    /// </summary>
    public class TargetServices : BaseService, ITargetServices
    {
        public List<TargetItem> GetAllTarget()
        {
            var targetList = new List<TargetItem>();
            //// pseudo data
            for (var i = 0; i < 4; i++)
            {
                var targetItem = new TargetItem
                {
                    Name = "Target " + i,
                    TargetTypeName = "Form " + i,
                    ProjectName = "Project " + i,
                    MilestoneName = "Milestone " + i,
                    StartDate = "04/04/2014",
                    EndDate = "30/04/2014",
                    Status = "Status " + i
                };

                targetList.Add(targetItem);
            }

            return targetList;
        }
    }
}
