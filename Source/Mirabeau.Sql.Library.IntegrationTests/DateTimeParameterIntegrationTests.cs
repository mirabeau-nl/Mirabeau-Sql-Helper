using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Mirabeau.Sql.Library;
using NUnit.Framework;

namespace Mirabeau.MsSql.Library.IntegrationTests
{
    [TestFixture]
    public class DateTimeParameterIntegrationTests
    {
        private const string Connectionstring = "Server=TEST-DB1;Database=master;Integrated Security=true";

        private readonly MsSqlHelper _sqlHelper = new MsSqlHelper();


        [TestFixtureSetUp]
        public void Setup()
        {
            string createTable =
                "CREATE TABLE dbo.tmp_dates (id int not null, d1 date NULL, d2 datetime NULL, d3 datetime2(7) NULL, d4 datetimeoffset(7) NULL)";

            _sqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, createTable);
        }

        [Test]
        public void ShouldInsertUtchDatesCorrectly()
        {
            IList<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(1.CreateSqlParameter("id"));
            parameters.Add(DateTime.UtcNow.CreateSqlParameter("d1", SqlDbType.Date));
            parameters.Add(DateTime.UtcNow.CreateSqlParameter("d2", SqlDbType.DateTime));
            parameters.Add(DateTime.UtcNow.CreateSqlParameter("d3", SqlDbType.DateTime2));
            parameters.Add(DateTime.UtcNow.CreateSqlParameter("d4", SqlDbType.DateTimeOffset));

            string insert =
                "INSERT INTO dbo.tmp_dates (id, d1, d2, d3 , d4) VALUES(@id, @d1, @d2, @d3, @d4)";

            _sqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, insert, parameters);
        }

        [Test]
        public void ShouldInsertNowDatesCorrectly()
        {
            IList<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(2.CreateSqlParameter("id"));
            parameters.Add(DateTime.Now.CreateSqlParameter("d1", SqlDbType.Date));
            parameters.Add(DateTime.Now.CreateSqlParameter("d2", SqlDbType.DateTime));
            parameters.Add(DateTime.Now.CreateSqlParameter("d3", SqlDbType.DateTime2));
            parameters.Add(DateTime.Now.CreateSqlParameter("d4", SqlDbType.DateTimeOffset));

            string insert =
                "INSERT INTO dbo.tmp_dates (id, d1, d2, d3 , d4) VALUES(@id, @d1, @d2, @d3, @d4)";

            _sqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, insert, parameters);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            string dropTable = "drop table dbo.tmp_dates";
            _sqlHelper.ExecuteNonQuery(Connectionstring, CommandType.Text, dropTable);

        }
    }
}
