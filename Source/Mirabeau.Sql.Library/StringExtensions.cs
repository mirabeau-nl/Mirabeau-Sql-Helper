using System;

namespace Mirabeau.Sql.Library
{
    /// <summary>
    /// Extentions for string.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Replaces a single quote to a double single quote.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ReplaceSingleQuote(this string sql)
        {
            if (sql == null)
            {
                throw new ArgumentNullException("sql");
            }
            return sql.Replace("'", "''");
        }
    }
}
