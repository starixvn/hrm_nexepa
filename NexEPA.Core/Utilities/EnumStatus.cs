// --------------------------------------------------------------------------
// <copyright file="EnumStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.11">Created</change>
// </history>

namespace NexEPA.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;

    /// <summary>
    /// ENUM Status.
    /// </summary>
    public class EnumStatus
    {
        /// <summary>
        /// Get ENUM Description.
        /// </summary>
        /// <param name="enumType">ENUM type.</param>
        /// <returns>String description of an ENUM.</returns>
        public static string GetDescripton(Enum enumType)
        {
            FieldInfo fi = enumType.GetType().GetField(enumType.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return enumType.ToString();
            }
        }

        public static IEnumerable<SelectListModel> GetAllStatusOfProject()
        {
            IEnumerable<StatusProject> statusList = Enum.GetValues(typeof(StatusProject)).Cast<StatusProject>();
            IEnumerable<SelectListModel> allStatus = statusList.Select(x => new SelectListModel
            {
                Id = (int)x,
                Name = GetDescripton((StatusProject)x)
            });

            return allStatus;
        }

        public static IEnumerable<SelectListModel> GetAllStatus()
        {
            IEnumerable<Status> statusList = Enum.GetValues(typeof(Status)).Cast<Status>();
            IEnumerable<SelectListModel> allStatus = statusList.Select(x => new SelectListModel
            {
                Id = (int)x,
                Name = GetDescripton((Status)x)
            });

            return allStatus;
        }
    }
}
