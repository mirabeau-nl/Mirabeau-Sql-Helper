using System;
using System.Data;
using System.Data.Common;
using NUnit.Framework;

namespace Mirabeau.MySql.Library.IntegrationTests
{
    [TestFixture]
    public class DatabaseExecutionTests
    {
        private string connectionString;

        [Test]
        public void ShouldExecuteReader()
        {
            MySqlHelper mySqlHelper = new MySqlHelper();

            using (DbDataReader dataReader = mySqlHelper.ExecuteReader(connectionString, CommandType.Text, "SELECT * FROM information_schema.tables"))
            {
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        object dbValue = dataReader[i];
                    }
                }
            }
        }
    }
}
