using System;
using System.Data;
using System.Data.SqlClient;

using NUnit.Framework;

namespace Mirabeau.Sql.Library.UnitTests
{
    public class DatabaseHelperTests
    {
        [TestFixture]
        internal class ExecuteNonQueryArgumentTests
        {
            private readonly SqlConnection _connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldThrowArgumentExceptionForConnectionstring(string connectionstring)
            {
                Assert.Throws(typeof(ArgumentException), () => DatabaseHelper.ExecuteNonQuery(connectionstring, CommandType.Text, "select"));
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void AsyncShouldThrowArgumentExceptionForConnectionstring(string connectionstring)
            {
                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(connectionstring, CommandType.Text, "select"), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldNotThrowNullReferenceExceptionForSqlConnection()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.Throws(typeof(ArgumentNullException), () => DatabaseHelper.ExecuteNonQuery(connection, CommandType.Text, "select"));
            }

            [Test]
            public void AsyncShouldNotThrowNullReferenceExceptionForSqlConnection()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(connection, CommandType.Text, "select"), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldNotThrowNullReferenceExceptionForEmptyCommand(string command)
            {
                Assert.Throws(typeof(ArgumentException), () => DatabaseHelper.ExecuteNonQuery(_connection, CommandType.Text, command));
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void AsyncShouldNotThrowNullReferenceExceptionForEmptyCommand(string command)
            {
                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(_connection, CommandType.Text, command), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldNotThrowNullReferenceExceptionForEmptyConnectionAndNoParameters()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.Throws(typeof(ArgumentNullException), () => DatabaseHelper.ExecuteNonQuery(connection, "spname"));
            }

            [Test]
            public void AsyncShouldNotThrowNullReferenceExceptionForEmptyConnectionAndNoParameters()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(connection, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [Test]
            public void ShouldNotThrowNullReferenceExceptionForEmptyConnectionWithParameters()
            {
                SqlConnection connection = null;

                Assert.Throws(
                    typeof(ArgumentNullException),
                    // ReSharper disable once ExpressionIsAlwaysNull
                    () => DatabaseHelper.ExecuteNonQuery(connection, "spname", 0.CreateSqlParameter("parameter")));
            }

            [Test]
            public void AsyncShouldNotThrowNullReferenceExceptionForEmptyConnectionWithParameters()
            {
                SqlConnection connection = null;

                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(connection, "spname", 0.CreateSqlParameter("parameter")), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmpty(string storedProcedureName)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.Throws(typeof(ArgumentException), () => DatabaseHelper.ExecuteNonQuery(connection, storedProcedureName));
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void AsyncShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmpty(string storedProcedureName)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(connection, storedProcedureName), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldNotThrowArgumentNullExceptionWhenTransactionIsNull()
            {
                SqlTransaction transaction = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.Throws(typeof(ArgumentNullException), () => DatabaseHelper.ExecuteNonQuery(transaction, "spname"));
            }

            [Test]
            public void AsyncShouldThrowArgumentNullExceptionWhenTransactionIsNull()
            {
                SqlTransaction transaction = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(transaction, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            public void ShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmptyAndParametersAreGiven(string storedProcedure)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.Throws(typeof(ArgumentException), () => DatabaseHelper.ExecuteNonQuery(connection, storedProcedure, 1));
            }

            [TestCase(null)]
            [TestCase("")]
            public void AsyncShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmptyAndParametersAreGiven(string storedProcedure)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.That(async () => await DatabaseHelper.ExecuteNonQueryAsync(connection, storedProcedure, 1), Throws.TypeOf<ArgumentException>());
            }
        }
    }
}
