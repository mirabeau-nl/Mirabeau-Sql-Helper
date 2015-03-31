using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

// ReSharper disable ExpressionIsAlwaysNull
namespace Mirabeau.MsSql.Library.UnitTests
{
    /// <summary>
    /// Tests for the <see cref="SqlParameterExtensions"/>
    /// </summary>
    [TestFixture]
    public class SqlParameterExtensionsTests
    {
        /// <summary>
        /// Clone should return null when a null parameter is passed.
        /// </summary>
        [Test]
        public void ShouldReturnNullWhenCloneIsCalledWithNullParameter()
        {
            Assert.That(SqlParameterExtensions.Clone(null), Is.EqualTo(null));
        }

        /// <summary>
        /// Basic test for  <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateStringParameters()
        {
            string emptyString = null;
            SqlParameter sqlParameter1 = emptyString.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter1.Value, Is.EqualTo(DBNull.Value));
            Assert.That(sqlParameter1.ParameterName, Is.EqualTo("ParameterName"));

            emptyString = string.Empty;
            SqlParameter sqlParameter2 = emptyString.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.Value, Is.EqualTo(emptyString));
            Assert.That(sqlParameter2.ParameterName, Is.EqualTo("ParameterName"));

            const string otherValue = "AString";
            SqlParameter sqlParameter3 = otherValue.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter3.Value, Is.EqualTo(otherValue));
            Assert.That(sqlParameter3.ParameterName, Is.EqualTo("ParameterName"));
        }

        /// <summary>
        /// Basic test for  <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateIntegerParameters()
        {
            int? value = null;
            SqlParameter sqlParameter1 = value.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter1.Value, Is.EqualTo(DBNull.Value));
            Assert.That(sqlParameter1.ParameterName, Is.EqualTo("ParameterName"));

            int anotherValue = 3;
            SqlParameter sqlParameter2 = anotherValue.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.SqlDbType, Is.EqualTo(SqlDbType.Int));
            Assert.That(sqlParameter2.Value, Is.EqualTo(3));
            Assert.That(sqlParameter2.ParameterName, Is.EqualTo("ParameterName"));

            int? valueThree = 3;
            SqlParameter sqlParameter3 = valueThree.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.SqlDbType, Is.EqualTo(SqlDbType.Int));
            Assert.That(sqlParameter3.Value, Is.EqualTo(3));
            Assert.That(sqlParameter3.ParameterName, Is.EqualTo("ParameterName"));
        }

        /// <summary>
        /// Basic test for <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateBooleanParameters()
        {
            bool? value = null;
            SqlParameter sqlParameter1 = value.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter1.Value, Is.EqualTo(DBNull.Value));
            Assert.That(sqlParameter1.ParameterName, Is.EqualTo("ParameterName"));

            const bool anotherValue = true;
            SqlParameter sqlParameter2 = anotherValue.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.Value, Is.EqualTo(true));
            Assert.That(sqlParameter2.ParameterName, Is.EqualTo("ParameterName"));
        }

        /// <summary>
        /// Basic test for <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateByteParameters()
        {
            byte? value = null;
            SqlParameter sqlParameter1 = value.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter1.Value, Is.EqualTo(DBNull.Value));
            Assert.That(sqlParameter1.ParameterName, Is.EqualTo("ParameterName"));

            byte anotherValue = 2;
            SqlParameter sqlParameter2 = anotherValue.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.Value, Is.EqualTo(anotherValue));
            Assert.That(sqlParameter2.ParameterName, Is.EqualTo("ParameterName"));
        }

        /// <summary>
        /// Basic test for <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateDecimalParameters()
        {
            decimal? value = null;
            SqlParameter sqlParameter1 = value.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter1.Value, Is.EqualTo(DBNull.Value));
            Assert.That(sqlParameter1.ParameterName, Is.EqualTo("ParameterName"));

            decimal anotherValue = 2.321654m;
            SqlParameter sqlParameter2 = anotherValue.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.Value, Is.EqualTo(anotherValue));
            Assert.That(sqlParameter2.ParameterName, Is.EqualTo("ParameterName"));
        }

        /// <summary>
        /// Basic test for <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateLongParameters()
        {
            long? value = null;
            SqlParameter sqlParameter1 = value.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter1.Value, Is.EqualTo(DBNull.Value));
            Assert.That(sqlParameter1.ParameterName, Is.EqualTo("ParameterName"));

            long anotherValue = long.MaxValue;
            SqlParameter sqlParameter2 = anotherValue.CreateSqlParameter("ParameterName");
            Assert.That(sqlParameter2.Value, Is.EqualTo(anotherValue));
            Assert.That(sqlParameter2.ParameterName, Is.EqualTo("ParameterName"));

        }

        /// <summary>
        /// Basic test for  <see cref="SqlParameterExtensions"/>
        /// </summary>
        [Test]
        public void ShouldCreateParametersForNonDefaultValueType()
        {
            ulong otherValue = 123;
            SqlParameter sqlParameter4 = SqlParameterExtensions.CreateSqlParameter<ulong>(otherValue, "ParameterName");
            Assert.That(sqlParameter4.Value, Is.EqualTo(otherValue));
            Assert.That(sqlParameter4.ParameterName, Is.EqualTo("ParameterName"));
        }

        /// <summary>
        /// Test that null values cannot be passed.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotPassNullAsArgument()
        {
            string.Empty.CreateSqlParameter(null);
        }

        /// <summary>
        /// Test that null values cannot be passed.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotPassNullAsArgument2()
        {
            DateTime? when = null;
            when.CreateSqlParameter(null);
        }

        /// <summary>
        /// Test that hours are in 24hour notation
        /// </summary>
        [Test]
        public void ShouldUse24HourNotationForDates()
        {
            DateTime dateTime = new DateTime(2011, 10, 19, 16, 30, 25, 123);
            SqlParameter sqlParameter1 = dateTime.CreateSqlParameter("ParameterName", DbType.DateTime);
            Assert.That(sqlParameter1.Value, Is.EqualTo(dateTime));
        }

        /// <summary>
        /// Test that hours are in 24hour notation
        /// </summary>
        [Test]
        public void ShouldUse24HourNotationForNullableDates()
        {
            DateTime? nullableDateTime = new DateTime(2011, 10, 19, 16, 30, 25, 123);
            SqlParameter sqlParameter1 = nullableDateTime.CreateSqlParameter("ParameterName", DbType.DateTime);
            Assert.That(sqlParameter1.Value, Is.EqualTo(nullableDateTime.Value));
        }
    }
}
// ReSharper restore ExpressionIsAlwaysNull
