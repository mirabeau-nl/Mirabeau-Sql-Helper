// ===============================================================================
// Microsoft Data Access Application Block for .NET
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//
// SQLHelper.cs
//
// This file contains the implementations of the SqlHelper and SqlHelperParameterCache
// classes.
//
// For more information see the Data Access Application Block Implementation Overview. 
// ===============================================================================
// Release history
// VERSION DESCRIPTION
// 2.0     Added support for FillDataSet, UpdateDataSet and "Param" helper methods
//
// ===============================================================================
// Copyright (C) 2000-2001 Microsoft Corporation
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ==============================================================================
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Mirabeau.Sql.Library
{
    /// <summary>
    /// SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
    /// ability to discover parameters for stored procedures at run-time.
    /// </summary>
    internal static class SqlHelperParameterCache
    {
        #region private methods, variables, and constructors

        private static readonly ConcurrentDictionary<string, IList<SqlParameter>> paramCache = new ConcurrentDictionary<string, IList<SqlParameter>>();

        /// <summary>
        /// Resolve at run time the appropriate set of SqlParameters for a stored procedure
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
        /// <returns>The parameter array discovered.</returns>
        private static IList<SqlParameter> DiscoverSpParameterSet(SqlTransaction transaction, SqlConnection connection, string storedProcedureName, bool includeReturnValueParameter)
        {
            using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                cmd.CommandType = CommandType.StoredProcedure;
                if (transaction != null)
                {
                    cmd.Transaction = transaction;
                }

                SqlCommandBuilder.DeriveParameters(cmd);

                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }

                SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];

                cmd.Parameters.CopyTo(discoveredParameters, 0);

                return discoveredParameters;
            }
        }

        /// <summary>
        /// Deep copy of cached SqlParameter array
        /// </summary>
        /// <param name="originalParameters">The original parameters.</param>
        /// <returns>The <see cref="IList{SqlParameter}"/>.</returns>
        private static IList<SqlParameter> CloneParameters(IList<SqlParameter> originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Count];

            for (int i = 0; i < originalParameters.Count; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }

        #endregion private methods, variables, and constructors

        #region caching functions

        /// <summary>
        /// Add parameter array to the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters to be cached</param>
        internal static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            string hashKey = connectionString + ":" + commandText;

            paramCache[hashKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve a parameter array from the cache
        /// </summary>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An array of SqlParameters</returns>
        internal static IList<SqlParameter> GetCachedParameterSet(string connectionString, string commandText)
        {
            string hashKey = connectionString + ":" + commandText;

            IList<SqlParameter> cachedParameters = paramCache[hashKey];

            if (cachedParameters == null)
            {
                return null;
            }

            return CloneParameters(cachedParameters);
        }

        #endregion caching functions

        #region Parameter Discovery Functions

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a SqlConnection</param>
        /// <param name="storedProcdureName">The name of the stored procedure</param>
        /// <returns>The <see cref="IList{SqlParameter}"/>.</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        internal static IList<SqlParameter> GetSPParameterSet(string connectionString, string storedProcdureName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return GetSPParameterSet(null, connection, storedProcdureName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="storedProcdureName">The name of the stored procedure</param>
        /// <returns>The <see cref="IList{SqlParameter}"/>.</returns>
        internal static IList<SqlParameter> GetSPParameterSet(SqlTransaction transaction, SqlConnection connection, string storedProcdureName)
        {
            return GetSPParameterSet(transaction, connection, storedProcdureName, false);
        }

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction object</param>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A boolean value indicating whether the return value parameter should be included in the results</param>
        /// <returns>The <see cref="IList{SqlParameter}"/>.</returns>
        internal static IList<SqlParameter> GetSPParameterSet(SqlTransaction transaction, SqlConnection connection, string storedProcedureName, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection", String_Resources.CannotbeNull);
            }

            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "storedProcedureName");
            }

            string hashKey = connection.ConnectionString + ":" + storedProcedureName + 
                             (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");

            IList<SqlParameter> cachedParameters = null;
            if (paramCache.ContainsKey(hashKey))
            {
                cachedParameters = paramCache[hashKey];
            }

            if (cachedParameters == null)
            {
                cachedParameters =
                    paramCache[hashKey] =
                        DiscoverSpParameterSet(transaction, connection, storedProcedureName, includeReturnValueParameter);
            }

            return CloneParameters(cachedParameters);
        }

        #endregion Parameter Discovery Functions
    }
}