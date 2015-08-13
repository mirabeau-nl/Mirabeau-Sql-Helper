using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Helper class to create sql statements with parameter declaration and values.
    /// </summary>
    public static class SqlDebugHelper
    {
        /// <summary>
        /// Creates a SQL-string with the parameter declaration and the sql statement so it can be executed in Management studio.
        /// </summary>
        /// <param name="sql">The sql-query</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string CreateExecutableSqlStatement(string sql, IEnumerable<SqlParameter> parameters)
        {
            // Bold assumtion, if sql has no space, then it's a stored procedure.
            if (sql.Contains(" "))
            {
                return CreateExecutableQueryStatement(sql, parameters);
            }
            else
            {
                // Asume sp -> TODO:
                throw new NotImplementedException("TODO: SP running");
            }
        }

        private static string CreateExecutableQueryStatement(string sql, IEnumerable<SqlParameter> parameters)
        {
            string sqlString = String.Empty;
            foreach (var dbParameter in parameters)
            {
                sqlString += CreateParameterText(dbParameter);
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
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    retval = "'" + sqlParameter.Value.ToString().Replace("'", "''") + "'";
                    break;

                case SqlDbType.Bit:
                    bool b = Boolean.Parse(sqlParameter.Value.ToString());
                    retval = (b) ? "1" : "0";
                    break;

                default:
                    retval = sqlParameter.Value.ToString().Replace("'", "''");
                    break;
            }

            return retval;

        }
    }
}
