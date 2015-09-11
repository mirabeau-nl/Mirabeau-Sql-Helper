using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Xml;
using Mirabeau.Sql.Library;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Interface for the MsSql helper.
    /// </summary>
    public interface IMsSqlHelper : IDatabaseHelper
    {
        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        int ExecuteNonQuery(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(connString, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        Task<int> ExecuteNonQueryAsync(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        Task<int> ExecuteNonQueryAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        int ExecuteNonQuery(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified 
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;int&gt; result = ExecuteNonQueryAsync(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        Task<int> ExecuteNonQueryAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified 
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        int ExecuteNonQuery(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        DataSet ExecuteDataSet(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid SqlTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        DataSet ExecuteDataSet(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        DataSet ExecuteDataSet(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values. This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        SqlDataReader ExecuteReader(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;SqlDataReader&gt; result = ExecuteReaderAsync(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        Task<SqlDataReader> ExecuteReaderAsync(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;SqlDataReader&gt; result = ExecuteReaderAsync(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        Task<SqlDataReader> ExecuteReaderAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        SqlDataReader ExecuteReader(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;SqlDataReader&gt; result = ExecuteReaderAsync(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        Task<SqlDataReader> ExecuteReaderAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A SqlDataReader containing the resultset generated by the command</returns>
        SqlDataReader ExecuteReader(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        Task<T> ExecuteScalarAsync<T>(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        object ExecuteScalar(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        T ExecuteScalar<T>(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(connString, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        Task<object> ExecuteScalarAsync(string connectionString, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        object ExecuteScalar(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        T ExecuteScalar<T>(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        Task<object> ExecuteScalarAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(conn, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        Task<T> ExecuteScalarAsync<T>(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;object&gt; orderCount = ExecuteScalarAsync(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        Task<object> ExecuteScalarAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        object ExecuteScalar(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  int orderCount = ExecuteScalar&lt;int&gt;(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        T ExecuteScalar<T>(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;{T}&gt; orderCount = ExecuteScalarAsync&lt;{T}&gt;(trans, "GetOrderCount", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
        Task<T> ExecuteScalarAsync<T>(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(conn, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbConnection connection, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(conn, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure using "FOR XML AUTO"</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided DbTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified DbTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(trans, CommandType.StoredProcedure, "GetOrders", new DbParameter("@productId", 24));
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
        /// <param name="commandParameters">An array of DbParameters used to execute the command</param>
        /// <returns>An XmlReader containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  XmlReader r = ExecuteXmlReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        XmlReader ExecuteXmlReader(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;XmlReader&gt; r = ExecuteXmlReaderAsync(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A dataset containing the resultset generated by the command</returns>
        Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues);

        /// <summary>
        /// Inserts a list of data in bulk.
        /// </summary>
        /// <param name="data">The set of dataobjects you want to insert</param>
        /// <param name="sqlBulkCopy">The <see cref="SqlBulkCopy"/> class.</param>
        /// <typeparam name="T">The type of record you to insert</typeparam>
        void BulkInsert<T>(IEnumerable<T> data, SqlBulkCopy sqlBulkCopy);

        /// <summary>
        /// Insert data in bulk.
        /// </summary>
        /// <param name="data">The set of dataobjects you want to insert</param>
        /// <param name="sqlBulkCopy">The <see cref="SqlBulkCopy"/> class.</param>
        /// <typeparam name="T">The type of record you to insert</typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task BulkInsertAsync<T>(IEnumerable<T> data, SqlBulkCopy sqlBulkCopy);
    }
}