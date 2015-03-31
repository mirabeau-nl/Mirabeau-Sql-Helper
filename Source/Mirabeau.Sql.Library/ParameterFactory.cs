﻿using System;
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
        /// <returns></returns>
        public T CreateParameter(object theValue, string parameterName)
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

            return parameter;
        }

        private static void ValidateParameter(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName", String_Resources.CannotbeNullOrEmpty);
            }
        }
    }
}