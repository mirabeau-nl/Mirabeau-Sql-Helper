using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Mirabeau.Sql.Library
{
    /// <summary>
    /// Interface for the database helper class.
    /// </summary>
    public interface IDatabaseHelper
    {
        /// <summary>
        /// Gets or sets the command timeout property. Default value is read from config.
        /// </summary>
        /// <value>
        /// The command timeout.
        /// </value>
        int CommandTimeout { get; set; }

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
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText);

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
        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText);

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
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText);

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
        Task<int> ExecuteNonQueryAsync(DbConnection connection, CommandType commandType, string commandText);

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
        int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<int> ExecuteNonQueryAsync(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<int> ExecuteNonQueryAsync(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText);

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
        Task<int> ExecuteNonQueryAsync(DbTransaction transaction, CommandType commandType, string commandText);

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
        int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<int> ExecuteNonQueryAsync(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<int> ExecuteNonQueryAsync(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText);

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
        DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        DataSet ExecuteDataSet(DbConnection connection, CommandType commandType, string commandText);

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
        DataSet ExecuteDataSet(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        DataSet ExecuteDataSet(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText);

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
        DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        T ExecuteReader<T>(string connectionString, CommandType commandType, string commandText) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(string connectionString, CommandType commandType, string commandText) where T : DbDataReader;

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
        T ExecuteReader<T>(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters) where T : DbDataReader;

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
        T ExecuteReader<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters) where T : DbDataReader;

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
        T ExecuteReader<T>(DbConnection connection, CommandType commandType, string commandText) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(DbConnection connection, CommandType commandType, string commandText) where T : DbDataReader;

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
        T ExecuteReader<T>(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters) where T : DbDataReader;

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
        T ExecuteReader<T>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters)where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters) where T : DbDataReader;

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
        T ExecuteReader<T>(DbTransaction transaction, CommandType commandType, string commandText) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(DbTransaction transaction, CommandType commandType, string commandText) where T : DbDataReader;

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
        T ExecuteReader<T>(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters) where T : DbDataReader;

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
        T ExecuteReader<T>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters) where T : DbDataReader;

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
        Task<T> ExecuteReaderAsync<T>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters) where T : DbDataReader;

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
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText);

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
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText);

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
        Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText);

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
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<T> ExecuteScalarAsync<T>(string connectionString, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText);

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
        T ExecuteScalar<T>(DbConnection connection, CommandType commandType, string commandText);

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
        Task<object> ExecuteScalarAsync(DbConnection connection, CommandType commandType, string commandText);

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
        Task<T> ExecuteScalarAsync<T>(DbConnection connection, CommandType commandType, string commandText);

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
        object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        T ExecuteScalar<T>(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<object> ExecuteScalarAsync(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<T> ExecuteScalarAsync<T>(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        T ExecuteScalar<T>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<object> ExecuteScalarAsync(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<T> ExecuteScalarAsync<T>(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText);

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
        T ExecuteScalar<T>(DbTransaction transaction, CommandType commandType, string commandText);

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
        Task<object> ExecuteScalarAsync(DbTransaction transaction, CommandType commandType, string commandText);

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
        Task<T> ExecuteScalarAsync<T>(DbTransaction transaction, CommandType commandType, string commandText);

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
        object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        T ExecuteScalar<T>(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<object> ExecuteScalarAsync(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        Task<T> ExecuteScalarAsync<T>(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters);

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
        object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        T ExecuteScalar<T>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<object> ExecuteScalarAsync(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

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
        Task<T> ExecuteScalarAsync<T>(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters);

        /// <summary>
        /// Creates a new DbConnection.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <returns></returns>
        DbConnection CreateConnection(string connectionString);

        /// <summary>
        /// Creates a new DbCommand.
        /// </summary>
        /// <returns></returns>
        DbCommand CreateCommand();

        /// <summary>
        /// Creates a new DbDataAdapter.
        /// </summary>
        /// <param name="command">the <see cref="DbCommand"/>.</param>
        /// <returns></returns>
        DbDataAdapter CreateDataAdapter(DbCommand command);
    }
}