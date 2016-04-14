using System;
using Fanex.Data.Repository;

namespace NexEPA.Target.Criteria.Target.Target
{
    public class GetRedmineProjectTargetCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ProjectIds { get; set; }

        public override bool IsValid()
        {
            bool isValid = FromDate > DateTime.MinValue
                            && ToDate > DateTime.MinValue
                            && FromDate <= ToDate;

            return isValid;
        }

        public override string GetSettingKey()
        {
            return "TargetRedmine_Get";
        }
    }
}
