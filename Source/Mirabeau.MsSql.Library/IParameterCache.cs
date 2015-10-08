using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParameterCache
    {
        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        IList<SqlParameter> GetStoredProcedureParameterSet(string connectionString, string storedProcedureName);
        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="connection">The connection.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns></returns>
        IList<SqlParameter> GetStoredProcedureParameterSet(DbTransaction connectionString, DbConnection connection, string storedProcedureName);

        /// <summary>
        /// Retrieves the set of SqlParameters appropriate for the stored procedure
        /// </summary>
        /// <remarks>
        /// This method will query the database for this information, and then store it in a cache for future requests.
        /// </remarks>
        /// <param name="connectionString">A valid SqlTransaction object</param>
        /// <param name="connection">A valid SqlConnection object</param>
        /// <param name="storedProcedureName">The name of the stored procedure</param>
        /// <param name="includeReturnValueParameter">A boolean value indicating whether the return value parameter should be included in the results</param>
        /// <returns>The <see cref="IList{SqlParameter}"/>.</returns>
        IList<SqlParameter> GetStoredProcedureParameterSet(DbTransaction connectionString, DbConnection connection, string storedProcedureName, bool includeReturnValueParameter);
    }
}