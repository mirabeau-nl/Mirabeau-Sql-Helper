using System;
using System.Data;
using System.Data.SqlClient;
using Mirabeau.Sql.Library;
using NUnit.Framework;

namespace Mirabeau.MsSql.Library.UnitTests
{
    public class DatabaseHelperTests
    {
        [TestFixture]
        internal class ExecuteNonQueryArgumentTests
        {
            private readonly IMsSqlHelper _sqlHelper = new MsSqlHelper();

            private readonly SqlConnection _connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldThrowArgumentExceptionForConnectionstring(string connectionstring)
            {
                Assert.That(() => _sqlHelper.ExecuteNonQuery(connectionstring, CommandType.Text, "select"), Throws.TypeOf<ArgumentException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void AsyncShouldThrowArgumentExceptionForConnectionstring(string connectionstring)
            {
                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(connectionstring, CommandType.Text, "select"), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldNotThrowNullReferenceExceptionForSqlConnection()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => _sqlHelper.ExecuteNonQuery(connection, CommandType.Text, "select"), Throws.TypeOf<ArgumentNullException>());
            }

            [Test]
            public void AsyncShouldNotThrowNullReferenceExceptionForSqlConnection()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(connection, CommandType.Text, "select"), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldNotThrowNullReferenceExceptionForEmptyCommand(string command)
            {
                Assert.That(() => _sqlHelper.ExecuteNonQuery(_connection, CommandType.Text, command), Throws.TypeOf<ArgumentException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void AsyncShouldNotThrowNullReferenceExceptionForEmptyCommand(string command)
            {
                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(_connection, CommandType.Text, command), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldNotThrowNullReferenceExceptionForEmptyConnectionAndNoParameters()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => _sqlHelper.ExecuteNonQuery(connection, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [Test]
            public void AsyncShouldNotThrowNullReferenceExceptionForEmptyConnectionAndNoParameters()
            {
                SqlConnection connection = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(connection, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [Test]
            public void ShouldNotThrowNullReferenceExceptionForEmptyConnectionWithParameters()
            {
                SqlConnection connection = null;

                Assert.That(() => _sqlHelper.ExecuteNonQuery(connection, "spname", 0.CreateSqlParameter("parameter")), Throws.TypeOf<ArgumentNullException>());
            }

            [Test]
            public void AsyncShouldNotThrowNullReferenceExceptionForEmptyConnectionWithParameters()
            {
                SqlConnection connection = null;

                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(connection, "spname", 0.CreateSqlParameter("parameter")), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmpty(string storedProcedureName)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.That(() => _sqlHelper.ExecuteNonQuery(connection, storedProcedureName), Throws.TypeOf<ArgumentException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void AsyncShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmpty(string storedProcedureName)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(connection, storedProcedureName), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldNotThrowArgumentNullExceptionWhenTransactionIsNull()
            {
                SqlTransaction transaction = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(() => _sqlHelper.ExecuteNonQuery(transaction, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [Test]
            public void AsyncShouldThrowArgumentNullExceptionWhenTransactionIsNull()
            {
                SqlTransaction transaction = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(transaction, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            public void ShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmptyAndParametersAreGiven(string storedProcedure)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.That(() => _sqlHelper.ExecuteNonQuery(connection, storedProcedure, 1), Throws.TypeOf<ArgumentException>());
            }

            [TestCase(null)]
            [TestCase("")]
            public void AsyncShouldThrowArgumentExceptionWhenProcedureNameIsNullOrEmptyAndParametersAreGiven(string storedProcedure)
            {
                SqlConnection connection = new SqlConnection("Data Source=.; Initial Catalog=X; User Id=Y; Password=######;");

                Assert.That(async () => await _sqlHelper.ExecuteNonQueryAsync(connection, storedProcedure, 1), Throws.TypeOf<ArgumentException>());
            }

            [Test]
            public void ShouldThrowArgumentNullForConnectionExceptionForExecuteScalar()
            {
                SqlConnection connection = null;

                Assert.That(() => _sqlHelper.ExecuteScalar(connection, "spname"), Throws.TypeOf<ArgumentNullException>());
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("  ")]
            public void ShouldThrowArgumentExceptionForConnectionstringForExecuteScalar(string connectionstring)
            {
                Assert.That(() => _sqlHelper.ExecuteScalar(connectionstring, CommandType.Text, "select"), Throws.TypeOf<ArgumentException>());
            }
        }
    }
}
