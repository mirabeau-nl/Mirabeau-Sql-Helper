using System;
using System.Data.Common;
using Mirabeau.Sql.Library;
using MySql.Data.MySqlClient;

namespace Mirabeau.MySql.Library
{
    /// <summary>
    /// Helper class for MySql commands.
    /// </summary>
    public class MySqlHelper : DatabaseHelperBase, IMySqlHelper
    {
        /// <summary>
        /// Creates a new DbConnection.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <returns></returns>
        public override DbConnection CreateConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Creates a new DbCommand.
        /// </summary>
        /// <returns></returns>
        public override DbCommand CreateCommand()
        {
            return new MySqlCommand();
        }

        /// <summary>
        /// Creates a new DbDataAdapter.
        /// </summary>
        /// <param name="command">the <see cref="DbCommand" />.</param>
        /// <returns></returns>
        public override DbDataAdapter CreateDataAdapter(DbCommand command)
        {
            return new MySqlDataAdapter(command as MySqlCommand);
        }

        protected override T SqlCommandExecuter<T>(Func<T> action)
        {
            return action();
        }
    }
}
