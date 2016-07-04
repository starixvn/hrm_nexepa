// --------------------------------------------------------------------------
// <copyright file="RedmineYearMonthModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// RedmineYearMonthModel class.
    /// </summary>
    public class RedmineYearMonthModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public string MonthName { get; set; }

        public int DayCount { get; set; }
    }
}
