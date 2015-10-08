using Mirabeau.Sql;

using MySql.Data.MySqlClient;

namespace Mirabeau.MySql.Library
{
    /// <summary>
    /// Factory for creating MySqlParameters.
    /// </summary>
    public class MySqlParameterFactory : ParameterFactory<MySqlParameter>
    {
        internal override MySqlParameter CreateParameter()
        {
            return new MySqlParameter();
        }
    }
}