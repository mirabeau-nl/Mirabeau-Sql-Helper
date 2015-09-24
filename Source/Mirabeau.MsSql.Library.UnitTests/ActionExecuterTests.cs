using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace Mirabeau.MsSql.Library.UnitTests
{
    [TestFixture]
    public class ActionExecuterTests
    {
        [TearDown]
        public void TearDown()
        {
            DatabaseHelper.MsSqlHelper = new MsSqlHelper();
        }

        [Test]
        public void ShouldUseCustomSqlHelperAndUseCustomActionExecuter()
        {
            var logger = MockRepository.GenerateStrictMock<ICustomLogger>();
            logger.Expect(x => x.WriteLine(Arg<string>.Is.Anything));
            DatabaseHelper.MsSqlHelper = new CustomMsSqlHelper(logger);


            try
            {
                DatabaseHelper.ExecuteNonQuery("not a valid connection", "sp_");
            }
            catch (Exception)
            {
                
            }

            logger.VerifyAllExpectations();
        }

        private class CustomMsSqlHelper: MsSqlHelper
        {
            private readonly ICustomLogger _logger;

            public CustomMsSqlHelper(ICustomLogger logger)
            {
                _logger = logger;
            }

            /// <summary>
            /// Wrapper around the sql execution.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="action">The action.</param>
            /// <returns></returns>
            public override T ActionExecuter<T>(Func<T> action)
            {
                try
                {
                    return base.ActionExecuter(action);
                }
                catch (Exception exception)
                {
                    _logger.WriteLine(exception.ToString());
                    throw;
                }
            }
        }
    }

    public interface ICustomLogger
    {
        void WriteLine(string line);
    }
}
