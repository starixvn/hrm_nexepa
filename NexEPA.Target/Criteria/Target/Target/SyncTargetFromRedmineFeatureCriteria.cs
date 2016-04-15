using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.Data.Repository;
using NexEPA.Core.Extensions;
using NexEPA.Core.Models;

namespace NexEPA.Target.Criteria.Target.Target
{
    public class SyncTargetFromRedmineFeatureCriteria : CriteriaBase
    {
        private const string TargetFeatureDataTemplate = "<TargetFeature TargetId=\"{0}\" TargetName=\"{1}\" StartDate=\"{2}\" EndDate=\"{3}\" ProjectId=\"{4}\" " +
                                                            "FeatureId=\"{5}\" ></TargetFeature>";

        public string TargetFeatureData
        {
            get
            {
                string targetFeatureData = GenerateTargetFeatureData();

                return targetFeatureData;
            }
        }

        public int AddedBy { get; set; }

        [Output]
        public int ErrCode { get; set; }

        [Output]
        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public IList<CommonTargetModel> Targets { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(TargetFeatureData) && AddedBy > 0;
        }

        public override string GetSettingKey()
        {
            return "Target_SyncFromRedmineFeature";
        }

        private string GenerateTargetFeatureData()
        {
            string targetFeatureData = string.Empty;

            if (Targets.Count > 0)
            {
                var targetFeatureDataBuilder = new StringBuilder();

                targetFeatureDataBuilder.Append("<ROOT>");
                Targets = Targets.Where(x => !string.IsNullOrWhiteSpace(x.Name)
                                        && x.RedmineFeatureId.HasValue
                                        && x.RedmineFeatureId.Value > 0
                                        && x.ProjectId > 0).ToList();

                foreach (CommonTargetModel target in Targets)
                {
                    targetFeatureDataBuilder.AppendFormat(
                                        TargetFeatureDataTemplate,
                                        target.TargetId,
                                        target.Name,
                                        target.StartDate,
                                        target.EndDate,
                                        target.ProjectId,
                                        target.RedmineFeatureId);
                }

                targetFeatureDataBuilder.Append("</ROOT>");

                targetFeatureData = targetFeatureDataBuilder.ToString();
            }

            return targetFeatureData;
        }
    }
}
