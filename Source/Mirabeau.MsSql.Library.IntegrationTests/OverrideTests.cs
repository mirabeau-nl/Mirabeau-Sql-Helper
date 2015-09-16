using System;
using System.Data;
using NUnit.Framework;
using Rhino.Mocks;

namespace Mirabeau.MsSql.Library.IntegrationTests
{
    [TestFixture]
    public class OverrideTests
    {
        [Test]
        public void ShouldUseOverridenIMsSqlHelper()
        {
            var sqlHelper = MockRepository.GenerateMock<IMsSqlHelper>();
            sqlHelper.Expect(m => m.ExecuteDataSet("x", CommandType.Text, "y")).Repeat.Once();
            DatabaseHelper.MsSqlHelper = sqlHelper;
            DatabaseHelper.ExecuteDataSet("x", CommandType.Text, "y");
            sqlHelper.VerifyAllExpectations();
        }
    }
}
