using System;
using System.Data;
using System.Data.SqlClient;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Extension methods for <see cref="SqlParameter"/>.
    /// </summary>
    public static class SqlParameterExtensions
    {
        private static readonly SqlParameterFactory Factory = new SqlParameterFactory();

        /// <summary>
        /// Uses the <see cref="ICloneable"/>Interface to clone a <see cref="SqlParameter"/>.
        /// </summary>
        /// <param name="sqlParameter">The SQL parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter Clone(this SqlParameter sqlParameter)
        {
            ICloneable cloneable = sqlParameter;
            return cloneable == null ? null : cloneable.Clone() as SqlParameter;
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value.
        /// </summary>
        /// <param name="nullableObject">The nullable object.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter<T>(this T? nullableObject, string parameterName) where T : struct
        {
            return Factory.CreateParameter(nullableObject, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this string theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this int? theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this int theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this decimal? theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this decimal theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this bool theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="dateTimeType">Type of the SQL column.</param>
        /// <returns>
        /// The <see cref="SqlParameter" />
        /// </returns>
        /// <exception cref="System.ArgumentNullException">parameterName</exception>
        public static SqlParameter CreateSqlParameter(this DateTime theValue, string parameterName, SqlDbType dateTimeType)
        {
            SqlParameter sqlParameter = Factory.CreateParameter(theValue, parameterName);
            sqlParameter.SqlDbType = dateTimeType;
            return sqlParameter;
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this bool? theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="dateTimeType">Type of the SQL column.</param>
        /// <returns>
        /// The <see cref="SqlParameter" />
        /// </returns>
        /// <exception cref="System.ArgumentNullException">parameterName</exception>
        public static SqlParameter CreateSqlParameter(this DateTime? theValue, string parameterName, SqlDbType dateTimeType)
        {
            if (theValue.HasValue)
            {
                CreateSqlParameter(theValue.Value, parameterName, dateTimeType);
            }

            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this long? theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a <see cref="SqlParameter"/>.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this long theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }

        /// <summary>
        /// Creates a SqlParameter for a tinyint
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this byte theValue, string parameterName)
        {
            return Factory.CreateParameter(theValue, parameterName);
        }
    }
}