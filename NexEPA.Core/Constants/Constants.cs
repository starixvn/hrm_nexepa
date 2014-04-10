// --------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.28">Created</change>
//   <change who="Josh.Phan" date="2014.04.07">Changed</change>
// </history>

namespace NexEPA.Core.Constants
{
    /// <summary>
    /// Constants value.
    /// </summary>
    public class Constants
    {
        public const string CurrentSessionKey = "NexEPA.CurrentSession";

        public const string ShortDateStringFormat = "MMM dd,yyyy";

        public const string NewStatus = "New";
        public const string InProgressStatus = "In Progress";
        public const string PendingStatus = "Pending";
        public const string ResolvedStatus = "Resolved";
        public const string ClosedStatus = "Closed";

        public enum Status
        {
            /// <summary>
            /// New status.
            /// </summary>
            New = 0,

            /// <summary>
            /// In Progress status.
            /// </summary>
            InProgress = 1,

            /// <summary>
            /// Pending status.
            /// </summary>
            Pending = 2,

            /// <summary>
            /// Resolved status.
            /// </summary>
            Resolved = 3,

            /// <summary>
            /// Closed status.
            /// </summary>
            Closed = 4
        }
    }
}
