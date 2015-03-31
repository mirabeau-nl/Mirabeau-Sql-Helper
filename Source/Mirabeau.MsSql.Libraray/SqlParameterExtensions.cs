using System;
using System.Data;
using System.Data.SqlClient;
using Mirabeau.Sql.Library;

namespace Mirabeau.MsSql.Library
{
    /// <summary>
    /// Extension methods for <see cref="SqlParameter"/>.
    /// </summary>
    public static class SqlParameterExtensions
    {
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
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (!nullableObject.HasValue)
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return new SqlParameter(parameterName, nullableObject.Value);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this string theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (string.IsNullOrEmpty(theValue))
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this int? theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (theValue == null)
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this int theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this decimal? theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (theValue == null)
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this decimal theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a SqlParameter
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this bool theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            return new SqlParameter(parameterName, theValue);
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
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            SqlParameter parameter = new SqlParameter(parameterName, dateTimeType);
            parameter.Value = theValue;
            return parameter;
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this bool? theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (theValue == null)
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return new SqlParameter(parameterName, theValue);
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
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (theValue == null)
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return theValue.Value.CreateSqlParameter(parameterName, dateTimeType);
        }

        /// <summary>
        /// Creates a SqlParameter with either DBNull or a value
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this long? theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            if (theValue == null)
            {
                return new SqlParameter(parameterName, DBNull.Value);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a <see cref="SqlParameter"/>.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this long theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            return new SqlParameter(parameterName, theValue);
        }

        /// <summary>
        /// Creates a SqlParameter for a tinyint
        /// </summary>
        /// <param name="theValue">the value.</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <returns>The <see cref="SqlParameter"/></returns>
        public static SqlParameter CreateSqlParameter(this byte theValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }

            return new SqlParameter(parameterName, theValue);
        }
    }
}