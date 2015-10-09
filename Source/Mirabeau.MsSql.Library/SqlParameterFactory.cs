using System.Data.SqlClient;

using Mirabeau.Sql;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Factory for creating Sql Parameters.
    /// </summary>
    public class SqlParameterFactory : ParameterFactory<SqlParameter>
    {
        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <returns></returns>
        internal override SqlParameter CreateParameter()
        {
            return new SqlParameter();
        }
    }
}