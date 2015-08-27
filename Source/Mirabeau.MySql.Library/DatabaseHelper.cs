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

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

// ReSharper disable MethodOverloadWithOptionalParameter
namespace Mirabeau.MySql.Library
{
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, scalable best practices for common uses of SqlClient.
    /// </summary>
    /// <remarks>
    /// In the AppSettings there is an option to set the value of the SqlCommandTimeout, to change the default SQL timeout.
    /// </remarks>
    public static class DatabaseHelper
    {
        private static readonly IMySqlHelper MySqlHelper = new MySqlHelper();

        /// <summary>
        /// Gets or sets the command timeout property. Default value is read from config.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        public static int CommandTimeout
        {
            get { return MySqlHelper.CommandTimeout; }
            set { MySqlHelper.CommandTimeout = value; }
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
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteNonQuery(connectionString, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand asynchronous (that returns no resultset and takes no parameters) against the database specified in 
        /// the connection string
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(connectionString, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteNonQuery(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteNonQuery(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteNonQuery(connection, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(connection, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteNonQuery(transaction, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(trans, CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(transaction, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteNonQuery(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteNonQuery(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static async Task<int> ExecuteNonQueryAsync(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteNonQueryAsync(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
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
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteDataSet(connectionString, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteDataSet(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteDataSet(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteDataSet(connection, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteDataSet(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteDataSet(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteDataSet(transaction, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteDataSet(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        public static DataSet ExecuteDataSet(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteDataSet(transaction, commandType, commandText, commandParameters);
        }

        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader>(connectionString, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(connString, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader>(connectionString, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(connString, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader, MySqlParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(connString, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader, MySqlParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(connection, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader>(connection, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(conn, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader, MySqlParameter>(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(conn, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader, MySqlParameter>(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  MySqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader>(transaction, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader>(transaction, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   MySqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await ExecuteReaderAsync(transaction, commandType, commandText, commandParameters as IEnumerable<MySqlParameter>).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   MySqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static MySqlDataReader ExecuteReader(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteReader<MySqlDataReader, MySqlParameter>(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///   Task&lt;MySqlDataReader&gt; result = ExecuteReaderAsync(trans, CommandType.StoredProcedure, "GetOrders", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>A MySqlDataReader containing the resultset generated by the command</returns>
        public static async Task<MySqlDataReader> ExecuteReaderAsync(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteReaderAsync<MySqlDataReader, MySqlParameter>(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
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
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteScalar<object>(connectionString, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(string connectionString, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteScalar<TResult>(connectionString, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
        /// the connection string. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteScalarAsync(connectionString, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar<TResult, MySqlParameter>(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<object, MySqlParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(string connectionString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult, MySqlParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteScalar(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteScalar<TResult, MySqlParameter>(connectionString, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;T&gt; orderCount = ExecuteScalarAsync&lt;T&gt;(connString, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(string connectionString, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult, MySqlParameter>(connectionString, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteScalar(connection, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteScalar<TResult>(connection, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteScalarAsync(connection, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(MySqlConnection connection, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult>(connection, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar<TResult, MySqlParameter>(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(MySqlConnection connection, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult, MySqlParameter>(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteScalar(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar&lt;int&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteScalar<TResult, MySqlParameter>(connection, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid MySqlConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(MySqlConnection connection, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult, MySqlParameter>(connection, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteScalar(transaction, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return MySqlHelper.ExecuteScalar<TResult>(transaction, commandType, commandText);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteScalarAsync(transaction, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided MySqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, CommandType.StoredProcedure, "GetOrderCount");
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(MySqlTransaction transaction, CommandType commandType, string commandText)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult>(transaction, commandType, commandText).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return MySqlHelper.ExecuteScalar<TResult, MySqlParameter>(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult, MySqlParameter>(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteScalar(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static TResult ExecuteScalar<TResult>(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return MySqlHelper.ExecuteScalar<TResult, MySqlParameter>(transaction, commandType, commandText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<object> ExecuteScalarAsync(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, CommandType.StoredProcedure, "GetOrderCount", new MySqlParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid MySqlTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters used to execute the command</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        public static async Task<TResult> ExecuteScalarAsync<TResult>(MySqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<MySqlParameter> commandParameters)
        {
            return await MySqlHelper.ExecuteScalarAsync<TResult, MySqlParameter>(transaction, commandType, commandText, commandParameters).ConfigureAwait(false);
        }

        #endregion ExecuteScalar
    }
}
// ReSharper restore MethodOverloadWithOptionalParameter
