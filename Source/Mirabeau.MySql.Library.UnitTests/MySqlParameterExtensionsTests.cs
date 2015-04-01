using System;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Mirabeau.MySql.Library.UnitTests
{
    [TestFixture]
    public class MySqlParameterExtensionsTests
    {
        [Test]
        public void ShouldCreateMyParameterNotNullabeParamter()
        {
            MySqlParameter mySqlParameter = "aBc".CreateMySqlParameter("param1");

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo("aBc"));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.VarChar));
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
