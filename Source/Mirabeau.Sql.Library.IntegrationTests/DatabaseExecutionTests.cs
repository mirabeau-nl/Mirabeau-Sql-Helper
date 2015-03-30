using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Mirabeau.MsSql.Library;
using NUnit.Framework;

namespace Mirabeau.Sql.Library.IntegrationTests
{
    [TestFixture, Explicit("Requires SQL-server master database.")]
    public class DatabaseExecutionTests
    {
        // Set this connection string to any sql server and point to the master database.
        private const string Connectionstring = "Server=TEST-DB1;Database=master;Integrated Security=true";
        private const string StoredProcedureName = "[sys].[sp_oledb_language]";
        private const string Query = "SELECT COUNT(*) FROM sys.objects WHERE type = @type";
        private readonly IMsSqlHelper _sqlHelper = new MsSqlHelper();

        private SqlConnection _sqlConnection;

        [TestFixtureSetUp]
        public void SetUpFixture()
        {
            _sqlConnection = new SqlConnection(Connectionstring);
        }

        [Test]
        public void ShouldExecuteReader()
        {
            IEnumerable<SqlParameter> parametersList = new List<SqlParameter>();
            SqlParameter[] parametersArray = new SqlParameter[0];

            using (var reader = _sqlHelper.ExecuteReader(Connectionstring, "[sys].[sp_datatype_info]", -7, 1))
            {
                while (reader.Read())
                {
                }
            }

            using (var reader = _sqlHelper.ExecuteReader(_sqlConnection, CommandType.StoredProcedure, StoredProcedureName))
            {
                while (reader.Read())
                {
                }
            }

            using (var reader = _sqlHelper.ExecuteReader(_sqlConnection, CommandType.StoredProcedure, StoredProcedureName, parametersList))
            {
                while (reader.Read())
                {
                }
            }

            using (var reader = _sqlHelper.ExecuteReader(_sqlConnection, CommandType.StoredProcedure, StoredProcedureName, parametersArray))
            {
                while (reader.Read())
                {
                }
            }

            using (var reader = _sqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, StoredProcedureName))
            {
                while (reader.Read())
                {
                }
            }

            using (var reader = _sqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, StoredProcedureName, parametersList))
            {
                while (reader.Read())
                {
                }
            }

            using (var reader = _sqlHelper.ExecuteReader(Connectionstring, CommandType.StoredProcedure, StoredProcedureName, parametersArray))
            {
                while (reader.Read())
                {
                }
            }

            using (var transaction = _sqlConnection.BeginTransaction())
            {
                using (var reader = _sqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, StoredProcedureName))
                {
                    while (reader.Read())
                    {
                    }
                }

                using (var reader = _sqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, StoredProcedureName, parametersList))
                {
                    while (reader.Read())
                    {
                    }
                }

                using (var reader = _sqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, StoredProcedureName, parametersArray))
                {
                    while (reader.Read())
                    {
                    }
                }

                transaction.Rollback();
            }


        }

        [Test]
        public void ShouldExecuteReaderStoredSql()
        {
            using (var reader = _sqlHelper.ExecuteReader(Connectionstring, CommandType.Text, Query, "s".CreateSqlParameter("type")))
            {
                while (reader.Read())
                {
                }
            }
        }

        [Test]
        public async void ShouldExecuteReaderAsyncStoredProcedure()
        {
            using (DbDataReader sqlDataReader = await _sqlHelper.ExecuteReaderAsync(Connectionstring, CommandType.StoredProcedure, StoredProcedureName))
            {
                while (await sqlDataReader.ReadAsync())
                {
                }
            }
        }

        [Test]
        public void ShouldExecuteNonQueryStoredProcedure()
        {
            _sqlHelper.ExecuteNonQuery(Connectionstring, CommandType.StoredProcedure, StoredProcedureName);
        }

        [Test]
        public async void ShouldExecuteNonQueryAsyncStoredProcedure()
        {
            await _sqlHelper.ExecuteNonQueryAsync(Connectionstring, CommandType.StoredProcedure, StoredProcedureName);
        }

        [Test]
        public void ShouldExecuteDataSetStoredProcedure()
        {
            _sqlHelper.ExecuteDataSet(Connectionstring, CommandType.StoredProcedure, StoredProcedureName);
        }

        [Test]
        public void ShouldExecuteScalarQuery()
        {
            int i = (int)_sqlHelper.ExecuteScalar(Connectionstring, CommandType.Text, Query, "S".CreateSqlParameter("type"));

            Assert.That(i, Is.GreaterThan(0));
        }
    }
}
