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

        public const string ShortDateStringFormat = "dd MMM, yyyy";

        public const string DefaultGravatarUrl = "?d=mm&amp;f=y";

        public const string KeyCryptography = "NexEPA08062014";

        public const string EmployeeEPAScore = "<KPI KPIId='{0}' KPIGroupId='{1}' Score='{2}' Comment='{3}'></KPI>";

        public const string BeginRootTag = "<ROOT>";

        public const string EndRootTag = "</ROOT>";
    }
}
