// --------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.11">Created</change>
// </history>

namespace NexEPA.Core.Repositories
{
    using System;
    using System.Collections;
    using System.Linq;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Extensions;

    /// <summary>
    /// Repository Base.
    /// </summary>
    public class BaseRepository
    {
        public CommandResult Insert(ICriteria criteria)
        {
            return ExcuteNonQuery(criteria);
        }

        public CommandResult Update(ICriteria criteria)
        {
            return ExcuteNonQuery(criteria);
        }

        public CommandResult Delete(ICriteria criteria)
        {
            return ExcuteNonQuery(criteria);
        }

        /// <summary>
        /// Function to insert, update or delete.
        /// </summary>
        /// <param name="criteria">Criteria contains parameter throw stored procedure.</param>
        /// <returns>Command Result.</returns>
        private CommandResult ExcuteNonQuery(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var hash = criteria.GetSpParams();
                hash = (new ResultCriteria()).AddSpParams(hash);
                var result = db.ExecuteNonQuery(hash);
                if (result == 1)
                {
                    SetValueForOutputProperty(criteria, hash);
                }

                return GetCommandResult(hash);
            }
        }

        private CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }

        /// <summary>
        /// Function to set new value from Hash table to Criteria.
        /// </summary>
        /// <param name="criteria">The Criteria.</param>
        /// <param name="hash">The Hash table.</param>
        private void SetValueForOutputProperty(ICriteria criteria, Hashtable hash)
        {
            var properties = criteria.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(OutputAttribute))).ToList();

            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    var value = hash[property.Name.ToLower()];
                    property.SetValue(criteria, value, null);
                }
            }
        }
    }
}
