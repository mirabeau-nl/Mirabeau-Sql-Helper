using System.Data;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Mirabeau.MySql.Library.UnitTests
{
    [TestFixture]
    public class MySqlParameterFactoryTests
    {
        private readonly MySqlParameterFactory _factory = new MySqlParameterFactory();

        [Test]
        public void ShouldCreateParameter()
        {
            MySqlParameter mySqlParameter = _factory.CreateParameter(12, "param1", ParameterDirection.InputOutput);

            Assert.That(mySqlParameter.ParameterName, Is.EqualTo("param1"));
            Assert.That(mySqlParameter.Value, Is.EqualTo(12));
            Assert.That(mySqlParameter.MySqlDbType, Is.EqualTo(MySqlDbType.Int32));
            Assert.That(mySqlParameter.Direction, Is.EqualTo(ParameterDirection.InputOutput));
        }
    }
}
