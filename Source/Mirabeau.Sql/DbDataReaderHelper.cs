using System;

namespace Mirabeau.Sql
{
    /// <summary>
    /// Helper for the DatabaseDataReader object.
    /// </summary>
    public static class DbDataReaderHelper
    {
        /// <summary>
        /// Return a non nullable value of type &lt;T&gt;
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <typeparam name="T">The type.</typeparam>
        public static T GetDbValueOrDefaultForValueType<T>(this object theValue) where T : struct
        {
            if (theValue is DBNull)
            {
                return default(T);
            }

            return (T)theValue;
        }

        /// <summary>
        /// Return a nullable value of type &lt;T&gt;
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <typeparam name="T">The type.</typeparam>
        public static T? GetDbValueForNullableValueType<T>(this object theValue) where T : struct
        {
            if (theValue == DBNull.Value)
            {
                return null;
            }

            return (T?)theValue;
        }

        /// <summary>
        /// Return a nullable value of type &lt;T&gt;
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <typeparam name="T">The type.</typeparam>
        public static T GetDbValueOrNullForReferenceType<T>(this object theValue) where T : class
        {
            if (theValue is DBNull)
            {
                return null;
            }

            return (T)theValue;
        }
    }
}