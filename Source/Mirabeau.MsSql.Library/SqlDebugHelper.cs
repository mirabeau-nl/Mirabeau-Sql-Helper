using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Helper class to create sql statements with parameter declaration and values.
    /// </summary>
    public static class SqlDebugHelper
    {
        private static readonly CultureInfo FormatCulture = CultureInfo.InvariantCulture;

        /// <summary>
        /// Creates a SQL-string with the parameter declaration and the sql statement so it can be executed in Sql Server Management studio.
        /// </summary>
        /// <param name="sql">The sql-query</param>
        /// <param name="parameters">The SqlParameters used for the sql-statement</param>
        /// <returns></returns>
        public static string CreateExecutableSqlStatement(string sql, params SqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql");
            }
            return CreateExecutableSqlStatement(sql, GetCommandType(sql), parameters);
        }

        /// <summary>
        /// Creates a SQL-string with the parameter declaration and the sql statement so it can be executed in Sql Server Management studio.
        /// </summary>
        /// <param name="sql">The sql-query</param>
        /// <param name="parameters">The SqlParameters used for the sql-statement</param>
        /// <returns></returns>
        public static string CreateExecutableSqlStatement(string sql, IList<SqlParameter> parameters)
        {
            var sqlParameters = ConvertToParams(parameters);
            return CreateExecutableSqlStatement(sql, sqlParameters);
        }

        /// <summary>
        /// Creates a SQL-string with the parameter declaration and the sql statement so it can be executed in Sql Server Management studio.
        /// </summary>
        /// <param name="sql">The sql-query</param>
        /// <param name="commandType">The <see cref="CommandType"/>. Only StoredProcedure and Text are supported.</param>
        /// <param name="parameters">The SqlParameters used for the sql-statement</param>
        /// <returns></returns>
        public static string CreateExecutableSqlStatement(string sql, CommandType commandType, IList<SqlParameter> parameters)
        {
            var param = ConvertToParams(parameters);
            return CreateExecutableSqlStatement(sql, commandType, param);
        }

        /// <summary>
        /// Creates a SQL-string with the parameter declaration and the sql statement so it can be executed in Sql Server Management studio.
        /// </summary>
        /// <param name="sql">The sql-query</param>
        /// <param name="commandType">The <see cref="CommandType"/>. Only StoredProcedure and Text are supported.</param>
        /// <param name="parameters">The SqlParameters used for the sql-statement</param>
        /// <returns></returns>
        public static string CreateExecutableSqlStatement(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql");
            }

            var safeParameters = parameters;
            if (safeParameters != null)
            {
                safeParameters = safeParameters.Where(p => p != null).ToArray();
            }

            switch (commandType)
            {
                case CommandType.StoredProcedure:
                    return CreateExecutableStoredProcedureStatement(sql, safeParameters);
                case CommandType.Text:
                    return CreateExecutableQueryStatement(sql, safeParameters);
                default:
                    throw new NotSupportedException(string.Format(FormatCulture, "The command type {0} is not supported.", commandType));
            }
        }

        private static CommandType GetCommandType(string sql)
        {
            // Bold assumtion, if sql has no space, then it's a stored procedure.
            return sql.Contains(" ") ? CommandType.Text : CommandType.StoredProcedure;
        }

        private static SqlParameter[] ConvertToParams(IList<SqlParameter> sqlParameters)
        {
            SqlParameter[] parameters = null;
            if (sqlParameters != null)
            {
                parameters = sqlParameters.ToArray();
            }
            return parameters;
        }

        /// <summary>
        /// Creates a T-SQL notation for a stored procedure that can be executed in Sql server management studio.
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string CreateExecutableStoredProcedureStatement(string storedProcedureName, IEnumerable<SqlParameter> parameters)
        {
            IList<string> sqlParameters = new List<string>();
            if (parameters != null)
            {
                foreach (SqlParameter sqlParameter in parameters)
                {
                    string param = string.Format(FormatCulture, "@{0} = {1}", sqlParameter.ParameterName,
                        GetParameterValue(sqlParameter));
                    sqlParameters.Add(param);
                }
            }

            string spString = String.Format(FormatCulture, "EXEC {0} {1}", storedProcedureName, string.Join(", ", sqlParameters));
            return spString;

        }

        /// <summary>
        /// Creates a T-SQL notation for a SQL Query that can be executed in Sql server management studio.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string CreateExecutableQueryStatement(string sql, IEnumerable<SqlParameter> parameters)
        {
            string sqlString = String.Empty;
            if (parameters != null)
            {
                foreach (var dbParameter in parameters)
                {
                    sqlString += CreateParameterText(dbParameter);
                }
            }

            sqlString += sql;
            return sqlString;
        }

        private static string CreateParameterText(SqlParameter dbParameter)
        {
            string declare = string.Format(FormatCulture, "declare @{0} {1}", dbParameter.ParameterName, dbParameter.SqlDbType);
            if (dbParameter.Size > 0)
            {
                if (dbParameter.Precision > 0)
                {
                    declare += string.Format(FormatCulture, "({0},{1})", dbParameter.Size, dbParameter.Precision);
                }
                else
                {
                    declare += string.Format(FormatCulture, "({0})", dbParameter.Size);
                }
            }

            declare += " = ";
            declare += GetParameterValue(dbParameter);
            declare += Environment.NewLine;

            return declare;
        }

        private static string GetParameterValue(SqlParameter sqlParameter)
        {
            string retval;

            if (sqlParameter.Value == DBNull.Value)
            {
                return "null";
            }

            switch (sqlParameter.SqlDbType)
            {
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                    retval = string.Format(FormatCulture, "N'{0}'", sqlParameter.Value.ToString().ReplaceSingleQuote());
                    break;
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Time:
                    retval = string.Format(FormatCulture, "'{0}'", sqlParameter.Value.ToString().ReplaceSingleQuote());
                    break;
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    var iso8601DateTime = ((DateTime)sqlParameter.Value).ToString("O", FormatCulture);
                    retval = string.Format(FormatCulture, "convert(datetime,'{0}', 127)", iso8601DateTime);
                    break;
                case SqlDbType.Bit:
                    retval = (Boolean.Parse(sqlParameter.Value.ToString())) ? "1" : "0";
                    break;
                case SqlDbType.Decimal:
                    retval = ((decimal)sqlParameter.Value).ToString(FormatCulture);
                    break;
                default:
                    retval = sqlParameter.Value.ToString().ReplaceSingleQuote();
                    break;
            }

            return retval;
        }

        private static string ReplaceSingleQuote(this string sql)
        {
            return sql.Replace("'", "''");
        }
    }
}
