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

        private const ParameterDirection DefaultDirectionInputDirection = ParameterDirection.Input;

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
        /// <param name="direction">The direction of the paramter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter<T>(this T? nullableObject, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection) where T : struct
        {
            return Factory.CreateParameter(nullableObject, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this string theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this int? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this int theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this decimal? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this decimal theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this bool theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="dateTimeType">Type of the SQL column.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>
        /// The <see cref="SqlParameter" />
        /// </returns>
        /// <exception cref="System.ArgumentNullException">parameterName</exception>
        public static SqlParameter CreateSqlParameter(this DateTime theValue, string parameterName, SqlDbType dateTimeType, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            SqlParameter sqlParameter = Factory.CreateParameter(theValue, parameterName, direction);
            sqlParameter.SqlDbType = dateTimeType;
            return sqlParameter;
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this bool? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="dateTimeType">Type of the SQL column.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>
        /// The <see cref="SqlParameter" />
        /// </returns>
        /// <exception cref="System.ArgumentNullException">parameterName</exception>
        public static SqlParameter CreateSqlParameter(this DateTime? theValue, string parameterName, SqlDbType dateTimeType, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            if (theValue.HasValue)
            {
                CreateSqlParameter(theValue.Value, parameterName, dateTimeType, direction);
            }

            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this long? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a <see cref="SqlParameter"/>.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this long theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter for a tinyint
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this byte theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter for a tinyint
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this byte? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter for a Time
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this TimeSpan theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter for a Time
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this TimeSpan? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter for a float
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this float theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }

        /// <summary>
        /// Creates a SqlParameter for a float
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this float? theValue, string parameterName, ParameterDirection direction = DefaultDirectionInputDirection)
        {
            return Factory.CreateParameter(theValue, parameterName, direction);
        }
    }
}