using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Mirabeau.MsSql.Library.IntegrationTests
{
    [TestFixture, Explicit]
    public class BulkInsertTest
    {
        private string connectionString = "insert connection string here";

        [TestFixtureSetUp]
        public void Setup()
        {
            DatabaseHelper.ExecuteNonQuery(connectionString, CommandType.Text, Properties.Resources.CreateTable);
        }

        [Test]
        public void ShouldInsertBulk()
        {
            var people = CreateSamplePeople(10000);

            using (var bulkCopy = new SqlBulkCopy(connectionString))
            {
                bulkCopy.DestinationTableName = "People";
                bulkCopy.ColumnMappings.Add("Name", "NAME");
                bulkCopy.ColumnMappings.Add("DateOfBirth", "Date");

                new MsSqlHelper().BulkInsert(people, bulkCopy);
            }
        }

        private static IEnumerable<Person> CreateSamplePeople(int count)
        {
            return Enumerable.Range(1, count)
                .Select(i => new Person
                {
                    Name = "Person" + i,
                    DateOfBirth = new DateTime(
                        1950 + (i%50),
                        ((i*3)%12) + 1,
                        ((i*7)%29) + 1)
                });
        }
    }

    internal class Person
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
