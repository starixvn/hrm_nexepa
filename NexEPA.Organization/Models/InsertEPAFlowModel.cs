// --------------------------------------------------------------------------
// <copyright file="InsertEPAFlowModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.19">Created</change>
// </history>

namespace NexEPA.Organization.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NexEPA.Core.Enums;
    using NexEPA.Employee.Models;

    /// <summary>
    /// InsertEPAFlow Model.
    /// </summary>
    public class InsertEPAFlowModel
    {
        private const string _EPAFlow = "<Process ProcessId='{0}' EvaluatorId='{1}'></Process>";
        private const string _BeginRootTag = "<ROOT>";
        private const string _EndRootTag = "</ROOT>";

        public int GroupId { get; set; }

        public int DepartmentId { get; set; }

        public int EmployeeId { get; set; }

        public int FlowType { get; set; }

        public List<EmployeeModel> Admins { get; set; }

        public List<EmployeeModel> Evaluators { get; set; }

        public List<EmployeeModel> Reviewers { get; set; }

        public List<EmployeeModel> Selfs { get; set; }

        public string FlowDetails
        {
            get
            {
                var value = new StringBuilder(_BeginRootTag);
                if (Admins != null)
                {
                    foreach (var admin in Admins)
                    {
                        value.AppendFormat(_EPAFlow, (int)ProcessTypeEnum.Admin, admin.EmployeeId);
                    }
                }

                if (Selfs != null)
                {
                    foreach (var self in Selfs)
                    {
                        value.AppendFormat(_EPAFlow, (int)ProcessTypeEnum.Self, -Math.Abs(self.EmployeeId));
                    }
                }

                if (Evaluators != null)
                {
                    foreach (var evaluator in Evaluators)
                    {
                        value.AppendFormat(_EPAFlow, (int)ProcessTypeEnum.Evaluator, evaluator.EmployeeId);
                    }
                }

                if (Reviewers != null)
                {
                    foreach (var reviewer in Reviewers)
                    {
                        value.AppendFormat(_EPAFlow, (int)ProcessTypeEnum.Reviewer, reviewer.EmployeeId);
                    }
                }

                value.Append(_EndRootTag);

                return value.ToString();
            }
        }
    }
}
