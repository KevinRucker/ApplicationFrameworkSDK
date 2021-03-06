﻿// Author: Kevin Rucker
// License: BSD 3-Clause
// Copyright (c) 2019, Kevin Rucker
// All rights reserved.

// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
//
// 1. Redistributions of source code must retain the above copyright notice,
//    this list of conditions and the following disclaimer.
//
// 2. Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
// 3. Neither the name of the copyright holder nor the names of its contributors
//    may be used to endorse or promote products derived from this software without
//    specific prior written permission.
//
// Disclaimer:
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
// EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ApplicationFrameworkSDK.Common
{
    public enum DataProviders
    {
        [Description("System.Data.Odbc")]
        Odbc,
        [Description("System.Data.Oledb")]
        OleDb,
        [Description("System.Data.OracleClient")]
        OracleClient,
        [Description("Oracle.DataAccess.Client.OracleClientFactory")]
        ODPManaged,
        [Description("Oracle.DataAccess.Client.OracleClientFactory")]
        ODPUnmanaged,
        [Description("FirebirdSql.Data.FirebirdClient")]
        FireBird,
        [Description("Npgsql.NpgsqlFactory")]
        PostgresSQL,
        [Description("MySql.Data.MySqlClient")]
        MySQL,
        [Description("System.Data.SqlClient")]
        SqlServer
    }

    public static class Extensions
    {
        #region Enumeration Extensions

        /// <summary>
        /// Get enumeration description attribute value
        /// </summary>
        /// <typeparam name="T"><see cref="enum"/> type</typeparam>
        /// <param name="e">Selected parameter</param>
        /// <returns><see cref="string"/> containing description text</returns>
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                var type = e.GetType();
                var values = Enum.GetValues(type);
                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null;
        }

        #endregion Enumeration Extensions
    }
}
