using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using Mirabeau.MsSql.Library.UnitTests.Properties;
using NUnit.Framework;

namespace Mirabeau.MsSql.Library.UnitTests
{
    [TestFixture]
    public class SqlDebugHelperTests
    {
        [Test]
        public void ShouldCreateExecutableQuery()
        {
            float? nullable = null;
            decimal decimalValue = 123.456m;

            string sql =
                "Select * FROM [dbo].[TableX] Where Column1 > @value1 and Column2 = @value2 and Column3 = @value3 and Column4 = @value4";

            IList<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(0.CreateSqlParameter("value1"));
            parameters.Add("hello world".CreateSqlParameter("value2"));
            parameters.Add(nullable.CreateSqlParameter("value3"));
            parameters.Add(decimalValue.CreateSqlParameter("value4"));

            string executableSql = SqlDebugHelper.CreateExecutableSqlStatement(sql, parameters);
            Trace.WriteLine(executableSql);
            Assert.That(executableSql, Is.EqualTo(Resources.ExpectedSqlQuery));
        }

        [Test]
        public void ShouldCreateExecutableStoredProcedure()
        {
            float? nullable = null;
            decimal decimalValue = 123.456m;
            string sql = "sp_test";

            IList<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(0.CreateSqlParameter("value1"));
            parameters.Add("hello world".CreateSqlParameter("value2"));
            parameters.Add(nullable.CreateSqlParameter("value3"));
            parameters.Add(decimalValue.CreateSqlParameter("value4"));
            

            string executableSql = SqlDebugHelper.CreateExecutableSqlStatement(sql, parameters);
            Trace.WriteLine(executableSql);
            Assert.That(executableSql, Is.EqualTo("EXEC sp_test @value1 = 0, @value2 = N'hello world', @value3 = null, @value4 = 123.456"));
        }
    }
}
