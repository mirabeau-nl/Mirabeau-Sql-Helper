using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using AutoMapper;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Mirabeau.MySql.Library.IntegrationTests
{
    [TestFixture, Explicit("Requires MYSQL database.")]
    public class DatabaseExecutionTests
    {
        readonly MySqlHelper _mySqlHelper = new MySqlHelper();

        // Created a free account on db4free. It is slow, and unreliable.. but allows some basic query testing.
        private const string ConnectionString = "server=db4free.net;uid=testcodelib;pwd=testcodelib;database=testcodelib;";

        [TestFixtureSetUp]
        public void Setup()
        {
            _mySqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, Properties.Resources.CreateTable);

            Mapper.CreateMap<IDataReader, TempTable>();
        }

        [Test]
        public void ShouldInsertIntoTable()
        {
            IList<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add("my firstname".CreateMySqlParameter("firstname"));
            parameters.Add("my lastnname".CreateMySqlParameter("lastname"));
            parameters.Add("email@servername.net".CreateMySqlParameter("email"));
            parameters.Add(DateTime.Now.CreateMySqlParameter("regdate", MySqlDbType.DateTime));

            _mySqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, Properties.Resources.InsertIntoTable, parameters);

            using (DbDataReader dataReader = _mySqlHelper.ExecuteReader(ConnectionString, CommandType.Text, "SELECT * FROM tmp_unittest_table"))
            {
                List<TempTable> rows = Mapper.Map<IDataReader, List<TempTable>>(dataReader);

                Assert.That(rows, Has.Count.EqualTo(1));
            }
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _mySqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, "DROP TABLE tmp_unittest_table");
        }
    }
}
