using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml;
using Mirabeau.Sql.Library;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Helper class for MsSql commands.
    /// </summary>
    public class MsSqlHelper : DatabaseHelperBase, IMsSqlHelper
    {
        private readonly ParameterCache _parameterCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="MsSqlHelper"/> class.
        /// </summary>
        public MsSqlHelper()
            : this(new ParameterCache())
        {
        }

        internal MsSqlHelper(ParameterCache parameterCache)
            // ReSharper disable once RedundantBaseConstructorCall
            : base()
        {
            _parameterCache = parameterCache;
        }

        /// <summary>
        /// Creates a new DbConnection.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <returns></returns>
        public override DbConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Creates a new DbCommand.
        /// </summary>
        /// <returns></returns>
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// Creates a new DbDataAdapter.
        /// </summary>
        /// <param name="command">the <see cref="DbCommand" />.</param>
        /// <returns></returns>
        public override DbDataAdapter CreateDataAdapter(DbCommand command)
        {
            return new SqlDataAdapter(command as SqlCommand);
        }

        #region ExecuteNonQuery

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
        public int ExecuteNonQuery(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteNonQueryAsync(connectionString, storedProcedureName, parameterValues).TaskResult();
        }

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
        public async Task<int> ExecuteNonQueryAsync(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(connectionString, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteNonQueryAsync(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteNonQueryAsync(connectionString, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public async Task<int> ExecuteNonQueryAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(null, connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public int ExecuteNonQuery(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteNonQueryAsync(connection, storedProcedureName, parameterValues).TaskResult();
        }

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
        public async Task<int> ExecuteNonQueryAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteNonQueryAsync(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteNonQueryAsync(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public int ExecuteNonQuery(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteNonQueryAsync(transaction, storedProcedureName, parameterValues).TaskResult();
        }

        #endregion

        #region Execute Dataset

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
        public DataSet ExecuteDataSet(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(connectionString, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return ExecuteDataSet(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public DataSet ExecuteDataSet(SqlTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of SqlParameters
                return ExecuteDataSet(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Pass through the call providing null for the set of SqlParameters
            return ExecuteDataSet(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public DataSet ExecuteDataSet(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.InvariantCulture, String_Resources.ParameterCannotBeNull, "connection"));
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(null, connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return ExecuteDataSet(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return ExecuteDataSet(connection, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion

        #region ExecuteReader

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values. This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public DbDataReader ExecuteReader(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteReaderAsync(connectionString, storedProcedureName, parameterValues).TaskResult();
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
        /// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(connString, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connectionString">A valid connection string for a DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public async Task<DbDataReader> ExecuteReaderAsync(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(connectionString, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteReaderAsync(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteReaderAsync(connectionString, CommandType.StoredProcedure, storedProcedureName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public async Task<DbDataReader> ExecuteReaderAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(null, connection, storedProcedureName);

                AssignParameterValues(commandParameters, parameterValues);

                return await ExecuteReaderAsync(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Pass through the call providing null for the set of DbParameters
            return await ExecuteReaderAsync(connection, CommandType.StoredProcedure, storedProcedureName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified DbConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="connection">A valid DbConnection</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public DbDataReader ExecuteReader(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteReaderAsync(connection, storedProcedureName, parameterValues).TaskResult();
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  Task&lt;DbDataReader&gt; result = ExecuteReaderAsync(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public async Task<DbDataReader> ExecuteReaderAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                AssignParameterValues(commandParameters, parameterValues);

                return await ExecuteReaderAsync(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Pass through the call providing null for the set of DbParameters
            return await ExecuteReaderAsync(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
        /// DbTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// e.g.:  
        ///  DbDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
        /// </remarks>
        /// <param name="transaction">A valid DbTransaction</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>A DbDataReader containing the resultset generated by the command</returns>
        public DbDataReader ExecuteReader(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteReaderAsync(transaction, storedProcedureName, parameterValues).TaskResult();
        }

        #endregion

        #region ExecuteScalar

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
        public async Task<T> ExecuteScalarAsync<T>(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(connectionString, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteScalarAsync<T>(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteScalarAsync<T>(connectionString, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public object ExecuteScalar(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteScalar<object>(connectionString, storedProcedureName, parameterValues);
        }

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
        public T ExecuteScalar<T>(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteScalarAsync<T>(connectionString, storedProcedureName, parameterValues).TaskResult();
        }

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
        public async Task<object> ExecuteScalarAsync(string connectionString, string storedProcedureName, params object[] parameterValues)
        {
            return await ExecuteScalarAsync<object>(connectionString, storedProcedureName, parameterValues);
        }

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
        public object ExecuteScalar(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteScalar<object>(connection, storedProcedureName, parameterValues);
        }

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
        public T ExecuteScalar<T>(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteScalarAsync<T>(connection, storedProcedureName, parameterValues).TaskResult();
        }

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
        public async Task<object> ExecuteScalarAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            return await ExecuteScalarAsync<object>(connection, storedProcedureName, parameterValues);
        }

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
        public async Task<T> ExecuteScalarAsync<T>(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(null, connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteScalarAsync<T>(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteScalarAsync<T>(connection, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public async Task<object> ExecuteScalarAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteScalarAsync(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteScalarAsync(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public object ExecuteScalar(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteScalar<object>(transaction, storedProcedureName, parameterValues);
        }

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
        public T ExecuteScalar<T>(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            return ExecuteScalarAsync<T>(transaction, storedProcedureName, parameterValues).TaskResult();
        }



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
        public async Task<T> ExecuteScalarAsync<T>(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteScalarAsync<T>(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteScalarAsync<T>(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion

        #region ExecuteXmlReader

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
        public XmlReader ExecuteXmlReader(DbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return ExecuteXmlReader(connection, commandType, commandText, null);
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteXmlReaderAsync(connection, commandType, commandText, null);
        }

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
        public XmlReader ExecuteXmlReader(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteXmlReader(connection, commandType, commandText, commandParameters as IEnumerable<DbParameter>);
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            return await ExecuteXmlReaderAsync(connection, commandType, commandText, commandParameters as IEnumerable<DbParameter>);
        }

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
        public XmlReader ExecuteXmlReader(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters)
        {
            // Create a command and prepare it for execution
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                XmlReader retval = cmd.ExecuteXmlReader();

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
                return retval;
            }
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters)
        {
            // Create a command and prepare it for execution
            using (SqlCommand cmd = new SqlCommand())
            {
                await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                XmlReader retval = await cmd.ExecuteXmlReaderAsync();

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
                return retval;
            }
        }

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
        public XmlReader ExecuteXmlReader(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(null, connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return ExecuteXmlReader(connection, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbConnection connection, string storedProcedureName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(null, connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteXmlReaderAsync(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Otherwise we can just call the SP without params
            return await ExecuteXmlReaderAsync(connection, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public XmlReader ExecuteXmlReader(DbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return ExecuteXmlReader(transaction, commandType, commandText, null);
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of DbParameters
            return await ExecuteXmlReaderAsync(transaction, commandType, commandText, null);
        }

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
        public XmlReader ExecuteXmlReader(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            return ExecuteXmlReader(transaction, commandType, commandText, commandParameters as IEnumerable<DbParameter>);
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
        {
            return await ExecuteXmlReaderAsync(transaction, commandType, commandText, commandParameters as IEnumerable<DbParameter>);
        }

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
        public XmlReader ExecuteXmlReader(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            XmlReader retval;

            // Create a command and prepare it for execution
            using (SqlCommand cmd = new SqlCommand())
            {
                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                retval = cmd.ExecuteXmlReader();

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
            }

            return retval;
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, CommandType commandType, string commandText, IEnumerable<DbParameter> commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            XmlReader retval;

            // Create a command and prepare it for execution
            using (SqlCommand cmd = new SqlCommand())
            {
                await PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                // Create the DataAdapter & DataSet
                retval = await cmd.ExecuteXmlReaderAsync();

                // Detach the DbParameters from the command object, so they can be used again
                cmd.Parameters.Clear();
            }

            return retval;
        }

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
        public XmlReader ExecuteXmlReader(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Pass through the call providing null for the set of DbParameters
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

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
        public async Task<XmlReader> ExecuteXmlReaderAsync(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction", String_Resources.CannotbeNull);
            }

            // If we receive parameter values, we need to figure out where they go
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
                IList<DbParameter> commandParameters = _parameterCache.GetStoredProcedureParameterSet(transaction, transaction.Connection, storedProcedureName);

                // Assign the provided values to these parameters based on parameter order
                AssignParameterValues(commandParameters, parameterValues);

                // Call the overload that takes an array of DbParameters
                return await ExecuteXmlReaderAsync(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
            }

            // Pass through the call providing null for the set of DbParameters
            return await ExecuteXmlReaderAsync(transaction, CommandType.StoredProcedure, storedProcedureName);
        }

        #endregion ExecuteXmlReader
    }
}
