namespace NexEPA.Target.Models
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;

    public class ProjectTargetModel
    {
        public ProjectFilterModel Project { get; set; }

        public IList<CommonTargetModel> Targets { get; set; }
    }
}
