using System;
using System.Data;
using System.Data.Common;

namespace Mirabeau.Sql.Library
{
    /// <summary>
    /// Factory for creating <see cref="DbParameter"/>s.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="DbParameter"/>.</typeparam>
    public abstract class ParameterFactory<T> where T : DbParameter
    {
        internal abstract T CreateParameter();

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="theValue">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="direction">The direction of the parameter.</param>
        /// <returns></returns>
        public T CreateParameter(object theValue, string parameterName, ParameterDirection direction)
        {
            ValidateParameter(parameterName);

            object valueToUse = theValue;

            if (theValue == null)
            {
                valueToUse = DBNull.Value;
            }

            T parameter = CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = valueToUse;
            parameter.Direction = direction;

            return parameter;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void ValidateParameter(string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentException(String_Resources.CannotbeNullOrEmpty, "parameterName");
            }
        }
    }
}