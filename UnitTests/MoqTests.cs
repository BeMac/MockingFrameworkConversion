using System;
using NUnit.Framework;
using Rhino.Mocks;
using MockingFrameworkConversion;
using Moq;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace UnitTests
{
    [TestClass]
    public class MoqTests
    {
        Configuration _config = new Configuration();

        [TestMethod]
        public void Dispose()
        {
            try
            {
                Mock<IWebRequestHelper> mockWrapper = new Mock<IWebRequestHelper>();
                MainProgram testThread = new MainProgram(_config);
                testThread.Dispose(mockWrapper.Object);

                MoqHelper.AddDisposeExpectation(mockWrapper);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public void Login()
        {
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedRequestHelper;
                    
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        
                        RhinoHelper.AddLoginExpectation(mockedRequestHelper, _config);
                    }

                    MainProgram testThread = new MainProgram(_config);

                    string success = testThread.LoginHttp(mockedRequestHelper);
                    Assert.That(success == "success");
                });
        }
    }
}
