// -----------------------------------------------------------------------
// <copyright file="MemberEPAModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.22">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models.MyEPA
{
    using System;

    /// <summary>
    /// EPA of member model.
    /// </summary>
    public class MemberEPAModel
    {
        public int EmployeeId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public int Score { get; set; }

        public DateTime EvaluateDate { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public Target Target { get; set; }

        public Project Project { get; set; }

        public Milestone Milestone { get; set; }

        public Evaluator Evaluator { get; set; }
    }
}
