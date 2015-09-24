// ===============================================================================
// Microsoft Data Access Application Block for .NET
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//
// SQLHelper.cs
// This file contains the implementations of the SqlHelper and SqlHelperParameterCache
// classes.
//
// For more information see the Data Access Application Block Implementation Overview. 
// ===============================================================================
// Release history
// VERSION      DESCRIPTION
//   2.0        Added support for FillDataset, UpdateDataset and "Param" helper methods
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
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading.Tasks;

// ReSharper disable MethodOverloadWithOptionalParameter
namespace Mirabeau.Sql.Library
{
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, scalable best practices for common uses of SqlClient.
    /// </summary>
    /// <remarks>
    /// In the AppSettings there is an option to set the value of the SqlCommandTimeout, to change the default SQL timeout.
    /// </remarks>
    public abstract class DatabaseHelperBase : IDatabaseHelper
    {
        private static int _commandTimeout;

        /// <summary>
        /// Gets or sets a value indicating whether we've read the command-timeout value from the config file.
        /// This makes sure we only read the config once.
        /// </summary>
        /// <value>
        /// <c>true</c> if command-timeout value was read from config; otherwise, <c>false</c>.
        /// </value>
        public static bool TimeoutHasBeenDetermined { get; set; }

        /// <summary>
        /// Gets or sets the command timeout property. Default value is read from config.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public virtual int CommandTimeout
        {
            get
            {
                if (!TimeoutHasBeenDetermined)
                {
                    GetCommandTimeout();
                }

                return _commandTimeout;
            }

            set
            {
                _commandTimeout = value;
            }
        }

        #region ExecuteNonQuery

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQueryAsync(connectionString, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand asynchronous (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteNonQueryAsync<DbParameter>(connectionString, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery<TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync<TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery<TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync<TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "connectionString");
            }
            return await ActionExecuter(async () =>
            {
                using (DbConnection cn = ActionExecuter(() => CreateConnection(connectionString)))
                {
                    await cn.OpenAsync().ConfigureAwait(false);

                    // Call the overload that takes a connection in place of the connection string
                    return await ExecuteNonQueryAsync(cn, commandType, commandText, commandParameters).ConfigureAwait(false);
                }
            });
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQueryAsync(connection, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync(DbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters.
            return await ExecuteNonQueryAsync<DbParameter>(connection, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery<TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteNonQueryAsync(connection, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync<TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteNonQueryAsync(connection, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery<TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteNonQueryAsync(connection, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, CommandType.StoredProcedure, "PublishOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync<TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            // Create a command and prepare it for execution
            using (DbCommand cmd = CreateCommand())
            {
                await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters).ConfigureAwait(false);

                // Finally, execute the command
                int retval = await ActionExecuter(() => cmd.ExecuteNonQueryAsync().ConfigureAwait(false));

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
                return retval;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQueryAsync(transaction, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync(DbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteNonQueryAsync<DbParameter>(transaction, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual int ExecuteNonQuery<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public virtual async Task<int> ExecuteNonQueryAsync<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.ParameterCannotBeNull);
            }

            // Create a command and prepare it for execution
            using (DbCommand cmd = CreateCommand())
            {
                await PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters).ConfigureAwait(false);

                // Execute the query
                int retval = await ActionExecuter(() => cmd.ExecuteNonQueryAsync().ConfigureAwait(false));

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
                return retval;
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return ExecuteDataSet<DbParameter>(connectionString, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet<TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteDataSet(connectionString, commandType, commandText, commandParameters as IEnumerable<TParameter>);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet<TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "connectionString");
            }

            // Create & open a DbConnection, and dispose of it after we are done
            return ActionExecuter(() =>
            {
                using (DbConnection cn = CreateConnection(connectionString))
                {
                    cn.Open();

                    // Call the overload that takes a connection in place of the connection string
                    return ExecuteDataSet(cn, commandType, commandText, commandParameters);
                }
            });
            
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet(DbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return ExecuteDataSet<DbParameter>(connection, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet<TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteDataSet(connection, commandType, commandText, commandParameters as IEnumerable<TParameter>);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet<TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            // Create a command and prepare it for execution
            using (DbCommand cmd = CreateCommand())
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                using (DbDataAdapter da = CreateDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    ds.Locale = CultureInfo.CurrentCulture;

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the DbParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    // Geef de dataset terug.
                    return ds;
                }
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return ExecuteDataSet<DbParameter>(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteDataSet(transaction, commandType, commandText, commandParameters as IEnumerable<DbParameter>);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public virtual DataSet ExecuteDataSet<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // Create a command and prepare it for execution
            using (DbCommand cmd = CreateCommand())
            {
                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                using (DbDataAdapter da = CreateDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    ds.Locale = CultureInfo.CurrentCulture;

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the DbParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    // Return the dataset
                    return ds;
                }
            }
        }
        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        /// Create and prepare a SqlCommand, and call ExecuteReaderAsync with the appropriate CommandBehavior.
        /// </summary>
        /// <remarks>
        /// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
        /// If the caller provided the connection, we want to leave it to them to manage.
        /// </remarks>
        /// <param name="connection">A valid DbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid DbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by SqlHelper</param>
        /// <returns>DbDataReader containing the results of the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        private async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters, SqlConnectionOwnership connectionOwnership) where TReader : DbDataReader where TParameter : DbParameter
        {
            // Create a command and prepare it for execution
            DbCommand cmd = CreateCommand();
            await PrepareCommandAsync(cmd, connection, transaction, commandType, commandText, commandParameters).ConfigureAwait(false);

            // Create a reader
            DbDataReader dr;

            // Call ExecuteReader with the appropriate CommandBehavior
            if (connectionOwnership == SqlConnectionOwnership.External)
            {
                dr = await ActionExecuter(() => cmd.ExecuteReaderAsync().ConfigureAwait(false));
            }
            else
            {
                dr = await ActionExecuter(() => cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection));
            }

            // Detach the DbParameters from the command object, so they can be used again.
            cmd.Parameters.Clear();
            return dr as TReader;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader>(string connectionString, CommandType commandType, string commandText) where TReader : DbDataReader
        {
            return ExecuteReaderAsync<TReader>(connectionString, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader>(string connectionString, CommandType commandType, string commandText) where TReader : DbDataReader
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteReaderAsync<TReader, DbParameter>(connectionString, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        public virtual TReader ExecuteReader<TReader, TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return ExecuteReaderAsync<TReader, TParameter>(connectionString, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(connString, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        public virtual async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return await ExecuteReaderAsync<TReader, TParameter>(connectionString, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        public virtual TReader ExecuteReader<TReader, TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return ExecuteReaderAsync<TReader, TParameter>(connectionString, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(connString, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope")]
        public virtual async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "connectionString");
            }

            return await ActionExecuter(async () =>
            {
                // Create and open the sql connection.
                DbConnection cn = CreateConnection(connectionString);
                await cn.OpenAsync().ConfigureAwait(false);

                try
                {
                    // Call the private overload that takes an internally owned connection in place of the connection string
                    TReader reader = await ExecuteReaderAsync<TReader, TParameter>(cn, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal).ConfigureAwait(false);
                    return reader;
                }
                catch
                {
                    // If we fail to return the SqlDatReader, we need to close the connection ourselves
                    cn.Close();
                    throw;
                }
            });
            
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader>(DbConnection connection, CommandType commandType, string commandText) where TReader : DbDataReader
        {
            return ExecuteReaderAsync<TReader>(connection, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader>(DbConnection connection, CommandType commandType, string commandText) where TReader : DbDataReader
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteReaderAsync<TReader, DbParameter>(connection, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader, TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return ExecuteReaderAsync<TReader, TParameter>(connection, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return await ExecuteReaderAsync<TReader, TParameter>(connection, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader, TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return ExecuteReaderAsync<TReader, TParameter>(connection, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return await ExecuteReaderAsync<TReader, TParameter>(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.External).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader>(DbTransaction transaction, CommandType commandType, string commandText) where TReader : DbDataReader
        {
            return ExecuteReaderAsync<TReader>(transaction, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader>(DbTransaction transaction, CommandType commandType, string commandText) where TReader : DbDataReader
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteReaderAsync<TReader, DbParameter>(transaction, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   DbDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return ExecuteReaderAsync<TReader, TParameter>(transaction, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return await ExecuteReaderAsync<TReader, TParameter>(transaction, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   DbDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual TReader ExecuteReader<TReader, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            return ExecuteReaderAsync<TReader, TParameter>(transaction, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public virtual async Task<TReader> ExecuteReaderAsync<TReader, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters)
            where TReader : DbDataReader
            where TParameter : DbParameter
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // Pass through to private overload, indicating that the connection is owned by the caller
            return await ExecuteReaderAsync<TReader, TParameter>(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External).ConfigureAwait(false);
        }

        /// <summary>
        /// This enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Connection is owned and managed by SqlHelper</summary>
            Internal,

            /// <summary>Connection is owned and managed by the caller</summary>
            External
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar<object>(connectionString, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual TResult ExecuteScalar<TResult>(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalarAsync<TResult, DbParameter>(connectionString, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteScalarAsync<DbParameter>(connectionString, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar<TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalar<object, TParameter>(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual TResult ExecuteScalar<TResult, TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalarAsync<TResult, TParameter>(connectionString, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync<TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<object, TParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult, TParameter>(string connectionString, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<TResult, TParameter>(connectionString, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar<TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalar<object, TParameter>(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual TResult ExecuteScalar<TResult, TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalarAsync<TResult, TParameter>(connectionString, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync<TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<object, TParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;T&gt; orderCount = ExecuteScalarAsync&lt;T&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult, TParameter>(string connectionString, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "connectionString");
            }

            return await ActionExecuter(async () =>
            {
                // Create & open a SqlConnection, and dispose of it after we are done
                using (DbConnection cn = CreateConnection(connectionString))
                {
                    await cn.OpenAsync().ConfigureAwait(false);

                    // Stuur de aanvraag door naar de overload waaraan een connectie meegegeven kan worden.
                    return await ExecuteScalarAsync<TResult, TParameter>(cn, commandType, commandText, commandParameters).ConfigureAwait(false);
                }
            });
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar<object>(connection, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual T ExecuteScalar<T>(DbConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalarAsync<T>(connection, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync(DbConnection connection, CommandType commandType, string commandText)
        {
            return await ExecuteScalarAsync<object>(connection, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult>(DbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteScalarAsync<TResult, DbParameter>(connection, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar<TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalar<object, TParameter>(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual TResult ExecuteScalar<TResult, TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalarAsync<TResult, TParameter>(connection, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync<TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<object, TParameter>(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult, TParameter>(DbConnection connection, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<TResult, TParameter>(connection, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar<TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalar<object, TParameter>(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar&lt;int&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual TResult ExecuteScalar<TResult, TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalarAsync<TResult, TParameter>(connection, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync<TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<object, TParameter>(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult, TParameter>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            // Create a command and prepare it for execution
            using (DbCommand cmd = CreateCommand())
            {
                await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters).ConfigureAwait(false);

                // Execute the command & return the results
                object retval = await ActionExecuter(() => cmd.ExecuteScalarAsync().ConfigureAwait(false));

                // Wis de DbParameters van het command object, zodat ze opnieuw kunnen worden gebruikt.
                cmd.Parameters.Clear();
                return (TResult)retval;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar<object>(transaction, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual T ExecuteScalar<T>(DbTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalarAsync<T>(transaction, commandType, commandText).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync(DbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteScalarAsync<object>(transaction, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult>(DbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteScalarAsync<TResult, DbParameter>(transaction, commandType, commandText, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalar<object, TParameter>(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual TResult ExecuteScalar<TResult, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalarAsync<TResult, TParameter>(transaction, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<object, TParameter>(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<TResult> ExecuteScalarAsync<TResult, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, params TParameter[] commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<TResult, TParameter>(transaction, commandType, commandText, commandParameters as IEnumerable<TParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual object ExecuteScalar<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalar<object, TParameter>(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual T ExecuteScalar<T, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return ExecuteScalarAsync<T, TParameter>(transaction, commandType, commandText, commandParameters).TaskResult();
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<object> ExecuteScalarAsync<TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            return await ExecuteScalarAsync<object, TParameter>(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public virtual async Task<T> ExecuteScalarAsync<T, TParameter>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // Create a command and prepare it for execution
            using (DbCommand cmd = CreateCommand())
            {
                await PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters).ConfigureAwait(false);

                // Create the DataAdapter & DataSet
                object retval = await ActionExecuter(() => cmd.ExecuteScalarAsync().ConfigureAwait(false));

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
                return (T)retval;
            }
        }

        #endregion ExecuteScalar

        #region private methods
        /// <summary>
        /// This method is used to attach array of DbParameters to a SqlCommand.
        /// This method will assign a value of DBNull to any parameter with a direction of
        /// InputOutput and a value of null.
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">
        /// The command to which the parameters will be added
        /// </param>
        /// <param name="commandParameters">
        /// An array of DbParameters to be added to command
        /// </param>
        private static void AttachParameters<TParameter>(DbCommand command, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            foreach (TParameter dbParameter in commandParameters)
            {
                // If dbparameter doens't have a value, set it to DBNull.
                if ((dbParameter.Direction == ParameterDirection.Input || dbParameter.Direction == ParameterDirection.InputOutput) && (dbParameter.Value == null))
                {
                    dbParameter.Value = DBNull.Value;
                }

                command.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of DbParameters
        /// </summary>
        /// <param name="commandParameters">Array of DbParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        internal static void AssignParameterValues(IList<SqlParameter> commandParameters, IList<object> parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                // Do nothing if we get no data
                return;
            }

            // The counts should match.
            if (commandParameters.Count != parameterValues.Count)
            {
                throw new ArgumentException(String_Resources.ParameterCountDoesNotMatch);
            }

            // Copy the parameters to the the command parameters.
            for (int i = 0; i < commandParameters.Count; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
        /// to the provided command
        /// </summary>
        /// <param name="command">The SqlCommand to be prepared</param>
        /// <param name="connection">A valid DbConnection, on which to execute this command</param>
        /// <param name="transaction">A valid DbTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of DbParameters to be associated with the command or 'null' if no parameters are required</param>
        internal async Task PrepareCommandAsync<TParameter>(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<TParameter> commandParameters) where TParameter : DbParameter
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection", String_Resources.CannotbeNull);
            }

            if (string.IsNullOrWhiteSpace(commandText))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "commandText");
            }

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync().ConfigureAwait(false);
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Read the timeout from the config.
            command.CommandTimeout = CommandTimeout;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        internal void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters)
        {
#pragma warning disable 4014
            PrepareCommandAsync(command, connection, transaction, commandType, commandText, commandParameters);
#pragma warning restore 4014
        }

        /// <summary>
        /// Reads the 'SqlCommandTimeout' from the appsettings section of the config file.
        /// If it exits it will try to convert it to an integer and will be set as CommandTimeout.
        /// Otherwise the default value will be set.
        /// </summary>
        private void GetCommandTimeout()
        {
            // Read it only once.
            if (!TimeoutHasBeenDetermined)
            {
                if (ConfigurationManager.AppSettings["SqlCommandTimeout"] != null)
                {
                    string timeoutConfig = ConfigurationManager.AppSettings["SqlCommandTimeout"];
                    if (!string.IsNullOrEmpty(timeoutConfig))
                    {
                        int timeout;
                        if (int.TryParse(timeoutConfig, out timeout))
                        {
                            // Stel de timeout waarde in op het command object.
                            CommandTimeout = timeout;
                            TimeoutHasBeenDetermined = true;
                        }
                    }
                }

                if (!TimeoutHasBeenDetermined)
                {
                    // Zet de default timeout.
                    using (DbCommand sqlCommand = CreateCommand())
                    {
                        CommandTimeout = sqlCommand.CommandTimeout;
                        TimeoutHasBeenDetermined = true;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Creates a new DbConnection.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <returns></returns>
        public abstract DbConnection CreateConnection(string connectionString);

        /// <summary>
        /// Creates a new DbCommand.
        /// </summary>
        /// <returns></returns>
        public abstract DbCommand CreateCommand();

        /// <summary>
        /// Creates a new DbDataAdapter.
        /// </summary>
        /// <param name="command">the <see cref="DbCommand"/>.</param>
        /// <returns></returns>
        public abstract DbDataAdapter CreateDataAdapter(DbCommand command);

        /// <summary>
        /// Wrapper around the sql execution.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public virtual T ActionExecuter<T>(Func<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            return action();
        }
    }
}
// ReSharper restore MethodOverloadWithOptionalParameter