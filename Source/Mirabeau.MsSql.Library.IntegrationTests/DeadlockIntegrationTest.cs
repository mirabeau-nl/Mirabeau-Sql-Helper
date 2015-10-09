﻿using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Mirabeau.MsSql.Library.IntegrationTests
{
    [TestFixture]
    public class DeadlockIntegrationTest
    {
        [Test, Ignore]
        public async void ShouldNotDeadlock()
        {
            // Not all library code used to have .ConfigureAwait(false);
            // This caused this code to run forever. Async test, opening async connection, running the sync db commands which use the async versions internally. 
            // See these topics for more information about .ConfigureAwait(false);
            // http://stackoverflow.com/questions/13489065/best-practice-to-call-configureawait-for-all-server-side-code/13489639#13489639
            // http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html

            string connectionstring = "Server=.;Database=master;Integrated Security=true";

            SqlConnection conn = new SqlConnection(connectionstring);
            await conn.OpenAsync();

            string query = "select top 1 TABLE_NAME from information_schema.tables";
            DatabaseHelper.ExecuteDataSet(connectionstring, CommandType.Text, query);
            DatabaseHelper.ExecuteNonQuery(connectionstring, CommandType.Text, query);
            DatabaseHelper.ExecuteScalar<string>(connectionstring, CommandType.Text, query);
            using (var dbReader = DatabaseHelper.ExecuteReader(connectionstring, CommandType.Text, query))
            {
                if (dbReader.HasRows)
                {
                    while (dbReader.Read())
                    {
                    }
                }
            }

        }
    }
}
