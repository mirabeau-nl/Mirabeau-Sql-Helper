using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Mirabeau.MsSql.Library.UnitTests
{
    [TestFixture]
    public class SqlDebugHelperTests
    {
        [Test]
        public void ShouldCreateExecutableQuery()
        {
            string sql =
                "Select * FROM [dbo].[TableX] Where Column1 > @value1 and Column2 = @value2";

            IList<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(0.CreateSqlParameter("value1"));
            parameters.Add("hello world".CreateSqlParameter("value2"));

            string executableSql = SqlDebugHelper.CreateExecutableSqlStatement(sql, parameters);
            Console.WriteLine(executableSql);
        }
    }
}
