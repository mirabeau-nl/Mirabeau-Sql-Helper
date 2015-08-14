using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Helper class to create sql statements with parameter declaration and values.
    /// </summary>
    public static class SqlDebugHelper
    {
        /// <summary>
        /// Creates a SQL-string with the parameter declaration and the sql statement so it can be executed in Sql Server Management studio.
        /// </summary>
        /// <param name="sql">The sql-query</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateExecutableSqlStatement(string sql, IEnumerable<SqlParameter> parameters)
        {
            // Bold assumtion, if sql has no space, then it's a stored procedure.
            return sql.Contains(" ") ? CreateExecutableQueryStatement(sql, parameters) : CreateExecutableStoredProcedureStatement(sql, parameters);
        }

        /// <summary>
        /// Creates a T-SQL notation for a stored procedure that can be executed in Sql server management studio.
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateExecutableStoredProcedureStatement(string storedProcedureName, IEnumerable<SqlParameter> parameters)
        {
            IList<string> sqlParameters = new List<string>();
            if (parameters != null)
            {
                foreach (SqlParameter sqlParameter in parameters)
                {
                    string param = string.Format("@{0} = {1}", sqlParameter.ParameterName,
                        GetParameterValue(sqlParameter));
                    sqlParameters.Add(param);
                }
            }

            string spString = String.Format("EXEC {0} {1}", storedProcedureName, string.Join(", ", sqlParameters));
            return spString;

        }

        /// <summary>
        /// Creates a T-SQL notation for a SQL Query that can be executed in Sql server management studio.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateExecutableQueryStatement(string sql, IEnumerable<SqlParameter> parameters)
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
            string declare = string.Format("declare @{0} {1}", dbParameter.ParameterName, dbParameter.SqlDbType);
            if (dbParameter.Size > 0)
            {
                if (dbParameter.Precision > 0)
                {
                    declare += string.Format("({0},{1})", dbParameter.Size, dbParameter.Precision);
                }
                else
                {
                    declare += string.Format("({0})", dbParameter.Size);
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
                    retval = string.Format("N'{0}'", sqlParameter.Value.ToString().ReplaceSingleQuote());
                    break;
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    retval = string.Format("'{0}'", sqlParameter.Value.ToString().ReplaceSingleQuote());
                    break;
                case SqlDbType.Bit:
                    retval = (Boolean.Parse(sqlParameter.Value.ToString())) ? "1" : "0";
                    break;
                case SqlDbType.Decimal:
                    retval = ((decimal)sqlParameter.Value).ToString(CultureInfo.InvariantCulture);
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
