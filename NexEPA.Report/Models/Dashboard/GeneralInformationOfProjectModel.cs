// -----------------------------------------------------------------------
// <copyright file="GeneralInformationOfProjectModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    /// <summary>
    /// General Information Of Project Model.
    /// </summary>
    public class GeneralInformationOfProjectModel
    {
        public int TotalFinishProject { get; set; }

        public int TotalInProgressProject { get; set; }
    }
}
