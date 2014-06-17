// -----------------------------------------------------------------------
// <copyright file="AppraisalFormModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.17">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    using System;

    /// <summary>
    /// The Appraisal Form model.
    /// </summary>
    public class AppraisalFormModel
    {
        public int Id { get; set; }

        public string EPAType { get; set; }

        public string Code { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public string FormName { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }
    }
}
