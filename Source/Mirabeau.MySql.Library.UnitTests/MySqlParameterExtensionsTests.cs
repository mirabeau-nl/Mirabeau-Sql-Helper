using System;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Mirabeau.MySql.Library.UnitTests
{
    [TestFixture]
    public class MySqlParameterExtensionsTests
    {
        [Test]
        public void ShouldCreateMyParameterNotNullabeParameterForString()
        {
            MySqlParameter mySqlParameter = "aBc".CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo("aBc"));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.VarChar));
        }

        [Test]
        public void ShouldCreateMyParameterNotNullabeParameterForInt()
        {
            int value = 1;
            MySqlParameter mySqlParameter = value.CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo(value));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.Int32));
        }

        [Test]
        public void ShouldCreateMyParameterNotNullabeParameterForLong()
        {
            long value = 1;
            MySqlParameter mySqlParameter = value.CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo(value));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.Int64));
        }

        [Test]
        public void ShouldCreateMyParameterNotNullabeParameterForDecimal()
        {
            decimal value = 1.2m;
            MySqlParameter mySqlParameter = value.CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo(value));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.Decimal));
        }

        [Test]
        public void ShouldCreateMyParameterNotNullabeParameterForBool()
        {
            bool value = true;
            MySqlParameter mySqlParameter = value.CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo(true));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.Byte));
        }

        [Test]
        public void ShouldCreateMyParameterNullabeParamter()
        {
            long? value = null;
            MySqlParameter mySqlParameter = value.CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo(DBNull.Value));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void ShouldThrowExceptionForInvalidArgument(string parameter)
        {
            Assert.That(() => 1.CreateMySqlParameter(parameter), Throws.Exception.TypeOf<ArgumentException>());
        }
    }
}
